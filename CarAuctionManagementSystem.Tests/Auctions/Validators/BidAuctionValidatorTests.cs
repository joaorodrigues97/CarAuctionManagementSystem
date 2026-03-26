using CarAuctionManagementSystem.Application.Auctions.Bid;
using CarAuctionManagementSystem.Domain.Auctions;
using FluentValidation;
using Moq;
using Xunit;

namespace CarAuctionManagementSystem.Tests.Auctions.Validators;

public class BidAuctionValidatorTests
{
    private readonly BidAuctionCommandValidator _validator = new();
    
    [Fact]
    public void Validator_ShouldReturnSuccess_IfBidAndVinAreValid()
    {
        // Arrange
        var command = new BidAuctionCommand(1000,
                                            "sdfsfsd");

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfBidIsEmpty()
    {
        // Arrange
        var command = new BidAuctionCommand(0,
                                            "sdfsfsd");

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Auctions.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Bid is a required field!");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfVinIsEmpty()
    {
        // Arrange
        var command = new BidAuctionCommand(1000,
                                            null);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Auctions.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "VIN is a required field!");
    }
}