using CarAuctionManagementSystem.Application.Auctions.StopAuction;
using CarAuctionManagementSystem.Application.Vehicles.AddVehicle;
using CarAuctionManagementSystem.Domain.Vehicles;
using Xunit;

namespace CarAuctionManagementSystem.Tests.Vehicles.Validators;

public class AddVehicleValidatorTests
{
    private readonly AddVehicleCommandValidator _validator = new();
    
    [Fact]
    public void Validator_ShouldReturnSuccess_IfAddVehicleIsValid()
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

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfIsSedanAndEmptyNumberOfDoors()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.Sedan,
                                            null,
                                            null,
                                            null, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Number of doors is a required field for Sedans or Hatchbacks!");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfIsHatchbackAndEmptyNumberOfDoors()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.Hatchback,
                                            null,
                                            null,
                                            null, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Number of doors is a required field for Sedans or Hatchbacks!");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfIsTruckAndHasNumberOfDoors()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.Truck,
                                            1,
                                            null,
                                            null, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Number of doors is not a valid field for SUV's or Trucks!");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfIsSuvAndHasNumberOfDoors()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.SUV,
                                            1,
                                            null,
                                            null, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Number of doors is not a valid field for SUV's or Trucks!");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfIsSuvAndEmptyNumberOfSeats()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.SUV,
                                            null,
                                            null,
                                            null, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Number of seats is a required field for SUV's!");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfIsTruckAndHasNumberOfSeats()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.Truck,
                                            null,
                                            1,
                                            null, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Number of seats is not a valid field for Hatchbacks, Sedans or Trucks!");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfIsSedanAndHasNumberOfSeats()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.Sedan,
                                            null,
                                            1,
                                            null, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Number of seats is not a valid field for Hatchbacks, Sedans or Trucks!");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfIsHatchbackAndHasNumberOfSeats()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.Hatchback,
                                            null,
                                            1,
                                            null, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Number of seats is not a valid field for Hatchbacks, Sedans or Trucks!");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfIsTruckAndEmptyLoadCapacity()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.Truck,
                                            null,
                                            null,
                                            null, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Load capacity is a required field for Trucks!");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfIsSuvAndHasLoadCapacity()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.SUV,
                                            null,
                                            1,
                                            1000, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Load capacity is not a valid field for Hatchbacks, Sedans or SUV's!");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfIsSedanAndHasLoadCapacity()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.Sedan,
                                            1,
                                            null,
                                            1000, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Load capacity is not a valid field for Hatchbacks, Sedans or SUV's!");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfIsHatchbackAndHasLoadCapacity()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.Hatchback,
                                            1,
                                            null,
                                            1000, 
                                            "sdgdsgdfss",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Load capacity is not a valid field for Hatchbacks, Sedans or SUV's!");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfVinIsEmpty()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.Hatchback,
                                            1,
                                            null,
                                            1000, 
                                            null,
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "VIN is a required field");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfManufacturerIsEmpty()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.Hatchback,
                                            1,
                                            null,
                                            1000, 
                                            "ahjdsafsadfsa",
                                            null,
                                            "S-MAX",
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Manufacturer is a required field");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfModelIsEmpty()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.Hatchback,
                                            1,
                                            null,
                                            1000, 
                                            "ahjdsafsadfsa",
                                            "Ford",
                                            null,
                                            2020,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Model is a required field");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfYearIsEmpty()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.Hatchback,
                                            1,
                                            null,
                                            1000, 
                                            "ahjdsafsadfsa",
                                            "Ford",
                                            "S-MAX",
                                            null,
                                            10000);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Year is a required field");
    }
    
    [Fact]
    public void Validator_ShouldReturnFailure_IfStartingBidIsEmpty()
    {
        // Arrange
        var command = new AddVehicleCommand(VehicleTypes.Hatchback,
                                            1,
                                            null,
                                            1000, 
                                            "ahjdsafsadfsa",
                                            "Ford",
                                            "S-MAX",
                                            2020,
                                            null);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorCode == "Vehicles.BadRequest");
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Starting bid is a required field");
    }
}