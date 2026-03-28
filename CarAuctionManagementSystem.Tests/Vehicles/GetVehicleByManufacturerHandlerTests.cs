using CarAuctionManagementSystem.Application.Vehicles.AddVehicle;
using CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByManufacturer;
using CarAuctionManagementSystem.Domain.Vehicles;
using FluentValidation;
using Moq;
using Xunit;

namespace CarAuctionManagementSystem.Tests.Vehicles;

public class GetVehicleByManufacturerHandlerTests
{
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly GetVehicleByManufacturerQueryHandler _handler;
    
    public GetVehicleByManufacturerHandlerTests()
    {
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        _handler = new GetVehicleByManufacturerQueryHandler(_vehicleRepositoryMock.Object);
    }
    
    [Fact]
    public void Handler_ShouldReturnSuccess_IfTheRequestIsCorrect()
    {
        // Arrange
        var command = new GetVehicleByManufacturerQuery("Ford");
        
        List<Vehicle> vehicle = [new()
        {
            VehicleType = VehicleTypes.SUV,
            NumberOfSeats = 1,
            Vin = "sdgdsgdfss",
            Manufacturer = "Ford",
            Model = "S-MAX",
            Year = 2020,
            Reserve = 10000
        }];
        
        _vehicleRepositoryMock.Setup(vehicleMock => vehicleMock.GetByManufacturer(command.Manufacturer, CancellationToken.None))
            .Returns(vehicle);

        // Act
        var result = _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(vehicle, result.Value);
    }
    
    [Fact]
    public void Handler_ShouldReturnSuccess_IfNoVehiclesFound()
    {
        // Arrange
        var command = new GetVehicleByManufacturerQuery("Ford");
        
        List<Vehicle> vehicle = [];
        
        _vehicleRepositoryMock.Setup(vehicleMock => vehicleMock.GetByManufacturer(command.Manufacturer, CancellationToken.None))
            .Returns(vehicle);

        // Act
        var result = _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "Vehicles.NotFound");
        Assert.Contains(result.Errors, error => error.Name == "No vehicles were found!");
    }
}