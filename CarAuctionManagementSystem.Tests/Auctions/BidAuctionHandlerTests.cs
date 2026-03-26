using CarAuctionManagementSystem.Application.Auctions.Bid;
using CarAuctionManagementSystem.Domain.Auctions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace CarAuctionManagementSystem.Tests.Auctions;

public class BidAuctionHandlerTests
{
    private readonly Mock<IAuctionRepository> _auctionRepositoryMock;
    private readonly Mock<IValidator<BidAuctionCommand>> _validatorMock;
    private readonly BidAuctionCommandHandler _handler;
    
    public BidAuctionHandlerTests()
    {
        _auctionRepositoryMock = new Mock<IAuctionRepository>();
        _validatorMock = new Mock<IValidator<BidAuctionCommand>>();
        _handler = new BidAuctionCommandHandler(_auctionRepositoryMock.Object, _validatorMock.Object);
    }
    
    [Fact]
    public void Handler_ShouldReturnSuccess_IfTheRequestIsCorrect()
    {
        // Arrange
        var command = new BidAuctionCommand(1000,
                                            "safsdfsdsf");
        var auction = new Auction
        {
            CurrentBid = 0,
            Vin = "safsdfsdsf",
            IsAuctionActive = false,
            LastBid = 0,
            StartingBid = 10000,
            StartDate = DateTime.Now,
            EndDate = null,
            MeetReserve = null
        };
        _validatorMock.Setup(validator => validator.Validate(command))
            .Returns(new ValidationResult());
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.GetAuctionByVin(command.Vin, CancellationToken.None))
            .Returns(auction);
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.IsAuctionActive(command.Vin, CancellationToken.None))
            .Returns(true);
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.IsBidValid(command.Vin, command.Bid, CancellationToken.None))
            .Returns(true);
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.Bid(command.Bid, command.Vin, CancellationToken.None))
            .Returns(true);

        // Act
        var result = _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }

    [Fact]
    public void Handler_ShouldReturnFailure_IfTheValidationFails()
    {
        // Arrange
        var command = new BidAuctionCommand(1000,
                                            "safsdfsdsf");

        ValidationResult validation = new ValidationResult
        {
            Errors = new List<ValidationFailure>
            {
                new ValidationFailure
                {
                    ErrorCode = "Auctions.BadRequest",
                    ErrorMessage = "Bid is a required field!"
                }
            }
        };
        
        _validatorMock.Setup(validator => validator.Validate(command)).Returns(validation);
        
        // Act
        var result = _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "Auctions.BadRequest");
        Assert.Contains(result.Errors, error => error.Name == "Bid is a required field!");
    }

    [Fact]
    public void Handler_ShouldReturnFailure_IfAuctionByVinFails()
    {
        // Arrange
        var command = new BidAuctionCommand(1000,
                                            "safsdfsdsf");
        Auction? auction = null;
        
        _validatorMock.Setup(validator => validator.Validate(command))
            .Returns(new ValidationResult());
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.GetAuctionByVin(command.Vin, CancellationToken.None))
            .Returns(auction);

        
        // Act
        var result = _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "Auctions.NotFound");
        Assert.Contains(result.Errors, error => error.Name == "No auctions were found!");
    }
    
    [Fact]
    public void Handler_ShouldReturnFailure_IfAuctionIsNotActive()
    {
        // Arrange
        var command = new BidAuctionCommand(1000,
                                            "safsdfsdsf");
        var auction = new Auction
        {
            CurrentBid = 0,
            Vin = "safsdfsdsf",
            IsAuctionActive = false,
            LastBid = 0,
            StartingBid = 10000,
            StartDate = DateTime.Now,
            EndDate = null,
            MeetReserve = null
        };
        
        _validatorMock.Setup(validator => validator.Validate(command))
            .Returns(new ValidationResult());
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.GetAuctionByVin(command.Vin, CancellationToken.None))
            .Returns(auction);
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.IsAuctionActive(command.Vin, CancellationToken.None))
            .Returns(false);
        
        // Act
        var result = _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "Auctions.BadRequest");
        Assert.Contains(result.Errors, error => error.Name == "Auction is no longer active!");
    }
    
    [Fact]
    public void Handler_ShouldReturnFailure_IfBidIsNotValid()
    {
        // Arrange
        var command = new BidAuctionCommand(1000,
                                            "safsdfsdsf");
        var auction = new Auction
        {
            CurrentBid = 0,
            Vin = "safsdfsdsf",
            IsAuctionActive = false,
            LastBid = 0,
            StartingBid = 10000,
            StartDate = DateTime.Now,
            EndDate = null,
            MeetReserve = null
        };
        
        _validatorMock.Setup(validator => validator.Validate(command))
            .Returns(new ValidationResult());
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.GetAuctionByVin(command.Vin, CancellationToken.None))
            .Returns(auction);
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.IsAuctionActive(command.Vin, CancellationToken.None))
            .Returns(true);
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.Bid(command.Bid, command.Vin, CancellationToken.None))
            .Returns(false);
        
        // Act
        var result = _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "Auctions.BadRequest");
        Assert.Contains(result.Errors, error => error.Name == "The bid you are trying to make is not valid!");
    }
}