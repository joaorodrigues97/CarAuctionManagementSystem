using CarAuctionManagementSystem.Application.Auctions.StartAuction;
using FluentValidation;

namespace CarAuctionManagementSystem.Application.Auctions.StopAuction;

public class StopAuctionCommandValidator : AbstractValidator<StopAuctionCommand>
{
    public StopAuctionCommandValidator()
    {
        RuleFor(input => input.Vin)
            .NotEmpty()
            .WithErrorCode("Auctions.BadRequest")
            .WithMessage("VIN is a required field!");
    }
}