using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Application.Abstractions.Utils;
using CarAuctionManagementSystem.Domain.Abstractions;
using CarAuctionManagementSystem.Domain.Auctions;
using FluentValidation;

namespace CarAuctionManagementSystem.Application.Auctions.StopAuction;

public sealed class StopAuctionCommandHandler(IAuctionRepository auctionRepository,
                                              IValidator<StopAuctionCommand> validator) : ICommandHandler<StopAuctionCommand, bool>
{
    public Result<bool> Handle(StopAuctionCommand command, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            return Result.Failure<bool>(validationResult.Errors.ConvertToError());
        }
        
        var auctionByVin = auctionRepository.GetByVin(command.Vin!);

        if (auctionByVin is null)
        {
            return Result.Failure<bool>([AuctionErrors.NotFound]);
        }
        
        Auction.StopAuction(auctionByVin);

        return Result.Success(true);
    }
}