using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Application.Abstractions.Utils;
using CarAuctionManagementSystem.Domain.Abstractions;
using CarAuctionManagementSystem.Domain.Auctions;
using CarAuctionManagementSystem.Domain.Vehicles;
using FluentValidation;

namespace CarAuctionManagementSystem.Application.Auctions.StartAuction;

public sealed class StartAuctionCommandHandler(IAuctionRepository auctionRepository,
                                               IVehicleRepository vehicleRepository,
                                               IValidator<StartAuctionCommand> validator) : ICommandHandler<StartAuctionCommand, bool>
{
    public Result<bool> Handle(StartAuctionCommand command, 
                               CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            return Result.Failure<bool>(validationResult.Errors.ConvertToError());
        }
        
        var vehicleByVin = vehicleRepository.GetVehicleByVin(command.Vin!);

        if (vehicleByVin is null)
        {
            return Result.Failure<bool>([AuctionErrors.VehicleNotExistsConflict]);
        }

        var auctionByVin = auctionRepository.GetAuctionByVin(command.Vin!);

        if (auctionByVin is not null)
        {
            return Result.Failure<bool>([AuctionErrors.AuctionConflict]);
        }
        
        Auction auction = new Auction
        {
            Vin = command.Vin,
            StartingBid = vehicleByVin.StartingBid
        };

        var result = auctionRepository.StartAuction(auction, cancellationToken);

        return Result.Create(result);
    }
}