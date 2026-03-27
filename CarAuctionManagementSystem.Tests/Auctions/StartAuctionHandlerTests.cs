using CarAuctionManagementSystem.Application.Auctions.Bid;
using CarAuctionManagementSystem.Application.Auctions.StartAuction;
using CarAuctionManagementSystem.Domain.Auctions;
using CarAuctionManagementSystem.Domain.Vehicles;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Quartz;
using Xunit;

namespace CarAuctionManagementSystem.Tests.Auctions;

public class StartAuctionHandlerTests
{
    private readonly Mock<IAuctionRepository> _auctionRepositoryMock;
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly Mock<IValidator<StartAuctionCommand>> _validatorMock;
    private readonly StartAuctionCommandHandler _handler;
    private readonly Mock<ISchedulerFactory> _schedulerFactoryMock;
    
    public StartAuctionHandlerTests()
    {
        _auctionRepositoryMock = new Mock<IAuctionRepository>();
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        _validatorMock = new Mock<IValidator<StartAuctionCommand>>();
        _schedulerFactoryMock = new Mock<ISchedulerFactory>();
        _handler = new StartAuctionCommandHandler(_auctionRepositoryMock.Object, _vehicleRepositoryMock.Object, _schedulerFactoryMock.Object, _validatorMock.Object);
    }
    
    [Fact]
    public void Handler_ShouldReturnSuccess_IfTheRequestIsCorrect()
    {
        // Arrange
        var command = new StartAuctionCommand("safsdfsdsf");
        
        Auction? nullAuction = null;
        
        var vehicle = new Vehicle
        {
            VehicleType = VehicleTypes.SUV,
            NumberOfSeats = 1,
            Vin = "sdgdsgdfss",
            Manufacturer = "Ford",
            Model = "S-MAX",
            Year = 2020,
            StartingBid = 10000
        };
        
        _validatorMock.Setup(validator => validator.Validate(command))
            .Returns(new ValidationResult());
        _vehicleRepositoryMock.Setup(vehicleMock => vehicleMock.GetVehicleByVin(command.Vin, CancellationToken.None))
            .Returns(vehicle);
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.GetAuctionByVin(command.Vin, CancellationToken.None))
            .Returns(nullAuction);
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.StartAuction(It.Is<Auction>(a => a.Vin == command.Vin), CancellationToken.None))
            .Returns(true);
        _schedulerFactoryMock.Setup(schedule => schedule.GetScheduler(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Mock<IScheduler>().Object);

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
        var command = new StartAuctionCommand("safsdfsdsf");
        
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
    public void Handler_ShouldReturnFailure_IfDoesNotFindVehicleByVin()
    {
        // Arrange
        var command = new StartAuctionCommand("safsdfsdsf");
        
        Vehicle? nullVehicle = null;
        
        _validatorMock.Setup(validator => validator.Validate(command))
            .Returns(new ValidationResult());
        _vehicleRepositoryMock.Setup(vehicleMock => vehicleMock.GetVehicleByVin(command!.Vin, CancellationToken.None))
            .Returns(nullVehicle);

        // Act
        var result = _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "Auctions.Conflict");
        Assert.Contains(result.Errors, error => error.Name == "Vehicle does not exists!");
    }
    
    [Fact]
    public void Handler_ShouldReturnFailure_IfFindsAuctionByVin()
    {
        // Arrange
        var command = new StartAuctionCommand("safsdfsdsf");

        Auction auction = new()
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
        
        var vehicle = new Vehicle
        {
            VehicleType = VehicleTypes.SUV,
            NumberOfSeats = 1,
            Vin = "sdgdsgdfss",
            Manufacturer = "Ford",
            Model = "S-MAX",
            Year = 2020,
            StartingBid = 10000
        };
        
        _validatorMock.Setup(validator => validator.Validate(command))
            .Returns(new ValidationResult());
        _vehicleRepositoryMock.Setup(vehicleMock => vehicleMock.GetVehicleByVin(command!.Vin, CancellationToken.None))
            .Returns(vehicle);
        _auctionRepositoryMock.Setup(auctionMock => auctionMock.GetAuctionByVin(command!.Vin, CancellationToken.None))
            .Returns(auction);

        // Act
        var result = _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "Auctions.Conflict");
        Assert.Contains(result.Errors, error => error.Name == "There's already an active auction for this vehicle!");
    }
}