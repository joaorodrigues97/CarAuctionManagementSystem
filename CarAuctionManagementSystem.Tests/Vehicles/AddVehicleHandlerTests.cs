using CarAuctionManagementSystem.Application.Auctions.StartAuction;
using CarAuctionManagementSystem.Application.Auctions.StopAuction;
using CarAuctionManagementSystem.Application.Vehicles.AddVehicle;
using CarAuctionManagementSystem.Domain.Auctions;
using CarAuctionManagementSystem.Domain.Vehicles;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace CarAuctionManagementSystem.Tests.Vehicles;

public class AddVehicleHandlerTests
{
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly Mock<IValidator<AddVehicleCommand>> _validatorMock;
    private readonly AddVehicleCommandHandler _handler;
    
    public AddVehicleHandlerTests()
    {
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        _validatorMock = new Mock<IValidator<AddVehicleCommand>>();
        _handler = new AddVehicleCommandHandler(_vehicleRepositoryMock.Object, _validatorMock.Object);
    }
    
    [Fact]
    public void Handler_ShouldReturnSuccess_IfTheRequestIsCorrect()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.SUV,
                                            null,
                                            1,
                                            null, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);

        Vehicle? nullVehicle = null;
        
        var vehicle = new Vehicle
        {
            VehicleType = VehicleTypes.SUV,
            NumberOfSeats = 1,
            Vin = "sdgdsgdfss",
            Manufacturer = "Ford",
            Model = "S-MAX",
            Year = 2020,
            Reserve = 10000
        };
        
        _validatorMock.Setup(validator => validator.Validate(command))
            .Returns(new ValidationResult());
        _vehicleRepositoryMock.Setup(vehicleMock => vehicleMock.GetByVin(command.Vin, CancellationToken.None))
            .Returns(nullVehicle);
        _vehicleRepositoryMock.Setup(auctionMock => auctionMock.Add(vehicle, CancellationToken.None))
            .Returns(true);

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
        var command = new AddVehicleCommand(VehicleTypes.SUV,
                                            null,
                                            1,
                                            null, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);
        
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
    public void Handler_ShouldReturnFailure_IfVehicleAlreadyExists()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.SUV,
                                            null,
                                            1,
                                            null, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);
        
        var vehicle = new Vehicle
        {
            VehicleType = VehicleTypes.SUV,
            NumberOfSeats = 1,
            Vin = "sdgdsgdfss",
            Manufacturer = "Ford",
            Model = "S-MAX",
            Year = 2020,
            Reserve = 10000
        };
        
        _validatorMock.Setup(validator => validator.Validate(command))
            .Returns(new ValidationResult());
        _vehicleRepositoryMock.Setup(vehicleMock => vehicleMock.GetByVin(command.Vin, CancellationToken.None))
            .Returns(vehicle);

        // Act
        var result = _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "Vehicles.Conflict");
        Assert.Contains(result.Errors, error => error.Name == "Vehicle already exists!");
    }
}