using CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByManufacturer;
using CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByType;
using CarAuctionManagementSystem.Domain.Vehicles;
using Moq;
using Xunit;

namespace CarAuctionManagementSystem.Tests.Vehicles;

public class GetVehicleByTypeHandlerTests
{
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly GetVehicleByTypeQueryHandler _handler;
    
    public GetVehicleByTypeHandlerTests()
    {
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        _handler = new GetVehicleByTypeQueryHandler(_vehicleRepositoryMock.Object);
    }
    
    [Fact]
    public void Handler_ShouldReturnSuccess_IfTheRequestIsCorrect()
    {
        // Arrange
        var command = new GetVehicleByTypeQuery(VehicleTypes.SUV);
        
        List<Vehicle> vehicle = [new()
        {
            VehicleType = VehicleTypes.SUV,
            NumberOfSeats = 1,
            Vin = "sdgdsgdfss",
            Manufacturer = "Ford",
            Model = "S-MAX",
            Year = 2020,
            StartingBid = 10000
        }];
        
        _vehicleRepositoryMock.Setup(vehicleMock => vehicleMock.GetVehicleByType(command.VehicleType, CancellationToken.None))
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
        var command = new GetVehicleByTypeQuery(VehicleTypes.SUV);
        
        List<Vehicle> vehicle = [];
        
        _vehicleRepositoryMock.Setup(vehicleMock => vehicleMock.GetVehicleByType(command.VehicleType, CancellationToken.None))
            .Returns(vehicle);

        // Act
        var result = _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "Vehicles.NotFound");
        Assert.Contains(result.Errors, error => error.Code == "No vehicles were found!");
    }
}