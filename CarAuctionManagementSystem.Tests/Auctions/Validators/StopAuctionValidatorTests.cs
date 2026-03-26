using CarAuctionManagementSystem.Application.Auctions.StartAuction;
using CarAuctionManagementSystem.Application.Auctions.StopAuction;
using Xunit;

namespace CarAuctionManagementSystem.Tests.Auctions.Validators;

public class StopAuctionValidatorTests
{
    private readonly StopAuctionCommandValidator _validator = new();
    
    [Fact]
    public void Validator_ShouldReturnSuccess_IfVinIsValid()
    {
        // Arrange
        var command = new StopAuctionCommand("sdfsfsd");

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfVinIsEmpty()
    {
        // Arrange
        var command = new StopAuctionCommand(null);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Auctions.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "VIN is a required field!");
    }
}