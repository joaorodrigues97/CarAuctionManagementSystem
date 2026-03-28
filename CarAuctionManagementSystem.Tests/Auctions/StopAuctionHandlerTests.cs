using CarAuctionManagementSystem.Application.Auctions.StartAuction;
using CarAuctionManagementSystem.Application.Auctions.StopAuction;
using CarAuctionManagementSystem.Domain.Auctions;
using CarAuctionManagementSystem.Domain.Vehicles;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace CarAuctionManagementSystem.Tests.Auctions;

public class StopAuctionHandlerTests
{
    private readonly Mock<IAuctionRepository> _auctionRepositoryMock;
    private readonly Mock<IValidator<StopAuctionCommand>> _validatorMock;
    private readonly StopAuctionCommandHandler _handler;
    
    public StopAuctionHandlerTests()
    {
        _auctionRepositoryMock = new Mock<IAuctionRepository>();
        _validatorMock = new Mock<IValidator<StopAuctionCommand>>();
        _handler = new StopAuctionCommandHandler(_auctionRepositoryMock.Object, _validatorMock.Object);
    }
    
    [Fact]
    public void Handler_ShouldReturnSuccess_IfTheRequestIsCorrect()
    {
        // Arrange
        var command = new StopAuctionCommand("safsdfsdsf");
        
        Auction auction = new()
        {
            CurrentBid = 0,
            Vin = "safsdfsdsf",
            IsAuctionActive = false,
            LastBid = 0,
            Reserve = 10000,
            StartDate = DateTime.Now,
            EndDate = null,
            MeetReserve = null
        };
        
        _validatorMock.Setup(validator => validator.Validate(command))
            .Returns(new ValidationResult());
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.GetByVin(command.Vin, CancellationToken.None))
            .Returns(auction);
        // Act
        var result = _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }
    
    [Fact]
    public void Handler_ShouldReturnFailure_IfTheValidatorFails()
    {
        // Arrange
        var command = new StopAuctionCommand("safsdfsdsf");
        
        ValidationResult validation = new ValidationResult
        {
            Errors = new List<ValidationFailure>
            {
                new ValidationFailure
                {
                    ErrorCode = "Auctions.BadRequest",
                    ErrorMessage = "VIN is a required field!"
                }
            }
        };
        
        _validatorMock.Setup(validator => validator.Validate(command))
            .Returns(validation);

        // Act
        var result = _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "Auctions.BadRequest");
        Assert.Contains(result.Errors, error => error.Name == "VIN is a required field!");
    }
    
    [Fact]
    public void Handler_ShouldReturnFailure_IfAuctionDoesNotExist()
    {
        // Arrange
        var command = new StopAuctionCommand("safsdfsdsf");

        Auction? nullAuction = null;
        
        _validatorMock.Setup(validator => validator.Validate(command))
            .Returns(new ValidationResult());
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.GetByVin(command.Vin, CancellationToken.None))
            .Returns(nullAuction);

        // Act
        var result = _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "Auctions.NotFound");
        Assert.Contains(result.Errors, error => error.Name == "No auctions were found!");
    }
}