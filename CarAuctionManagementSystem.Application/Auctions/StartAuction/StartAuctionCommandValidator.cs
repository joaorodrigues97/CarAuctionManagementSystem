using FluentValidation;

namespace CarAuctionManagementSystem.Application.Auctions.StartAuction;

public class StartAuctionCommandValidator : AbstractValidator<StartAuctionCommand>
{
    public StartAuctionCommandValidator()
    {
        RuleFor(input => input.Vin)
            .NotEmpty()
            .WithErrorCode("Auctions.BadRequest")
            .WithMessage("VIN is a required field!");
    }
}