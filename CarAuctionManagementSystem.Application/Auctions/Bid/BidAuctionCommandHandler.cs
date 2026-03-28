using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Application.Abstractions.Utils;
using CarAuctionManagementSystem.Domain.Abstractions;
using CarAuctionManagementSystem.Domain.Auctions;
using FluentValidation;

namespace CarAuctionManagementSystem.Application.Auctions.Bid;

public sealed class BidAuctionCommandHandler(IAuctionRepository auctionRepository,
                                             IValidator<BidAuctionCommand> validator) : ICommandHandler<BidAuctionCommand, bool>
{
    public Result<bool> Handle(BidAuctionCommand command, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            return Result.Failure<bool>(validationResult.Errors.ConvertToError());
        }
        
        Auction? auction = auctionRepository.GetByVin(command.Vin);

        if (auction is null)
        {
            return Result.Failure<bool>([AuctionErrors.NotFound]);
        }
        
        bool isAuctionActive = auctionRepository.IsActive(command.Vin);

        if (!isAuctionActive)
        {
            return Result.Failure<bool>([AuctionErrors.AuctionActive]);
        }

        bool isBidValid = auctionRepository.IsBidValid(command.Vin,
                                                       command.Bid);

        if (!isBidValid)
        {
            return Result.Failure<bool>([AuctionErrors.BidValid]);
        }

        var result = auctionRepository.Bid(command.Bid, 
                                                command.Vin);

        return Result.Success(result);
    }
}