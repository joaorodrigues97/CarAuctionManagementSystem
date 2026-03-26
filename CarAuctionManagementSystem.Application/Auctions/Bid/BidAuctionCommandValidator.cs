using FluentValidation;

namespace CarAuctionManagementSystem.Application.Auctions.Bid;

public class BidAuctionCommandValidator : AbstractValidator<BidAuctionCommand>
{
    public BidAuctionCommandValidator()
    {
        RuleFor(input => input.Bid)
            .NotEmpty()
            .WithErrorCode("Auctions.BadRequest")
            .WithMessage("Bid is a required field!");
        
        RuleFor(input => input.Vin)
            .NotEmpty()
            .WithErrorCode("Auctions.BadRequest")
            .WithMessage("VIN is a required field!");
    }
}