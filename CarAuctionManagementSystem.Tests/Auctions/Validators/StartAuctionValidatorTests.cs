using CarAuctionManagementSystem.Application.Auctions.Bid;
using CarAuctionManagementSystem.Application.Auctions.StartAuction;
using Xunit;

namespace CarAuctionManagementSystem.Tests.Auctions.Validators;

public class StartAuctionValidatorTests
{
    private readonly StartAuctionCommandValidator _validator = new();
    
    [Fact]
    public void Validator_ShouldReturnSuccess_IfStartingBidAndVinAreValid()
    {
        // Arrange
        var command = new StartAuctionCommand("sdfsfsd");

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfVinIsEmpty()
    {
        // Arrange
        var command = new StartAuctionCommand(null);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Auctions.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "VIN is a required field!");
    }
}