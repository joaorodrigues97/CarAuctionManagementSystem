using CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByManufacturer;
using CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByYear;
using CarAuctionManagementSystem.Domain.Vehicles;
using Moq;
using Xunit;

namespace CarAuctionManagementSystem.Tests.Vehicles;

public class GetVehicleByYearHandlerTests
{
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly GetVehicleByYearQueryHandler _handler;
    
    public GetVehicleByYearHandlerTests()
    {
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        _handler = new GetVehicleByYearQueryHandler(_vehicleRepositoryMock.Object);
    }
    
    [Fact]
    public void Handler_ShouldReturnSuccess_IfTheRequestIsCorrect()
    {
        // Arrange
        var command = new GetVehicleByYearQuery(2020);
        
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
        
        _vehicleRepositoryMock.Setup(vehicleMock => vehicleMock.GetVehicleByYear(command.Year, CancellationToken.None))
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
        var command = new GetVehicleByYearQuery(2020);
        
        List<Vehicle> vehicle = [];
        
        _vehicleRepositoryMock.Setup(vehicleMock => vehicleMock.GetVehicleByYear(command.Year, CancellationToken.None))
            .Returns(vehicle);

        // Act
        var result = _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "Vehicles.NotFound");
        Assert.Contains(result.Errors, error => error.Name == "No vehicles were found!");
    }
}