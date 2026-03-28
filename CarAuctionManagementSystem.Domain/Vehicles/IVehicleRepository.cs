namespace CarAuctionManagementSystem.Domain.Vehicles;

public interface IVehicleRepository
{
    bool Add(Vehicle vehicle,
        CancellationToken cancellationToken = default);
    
    List<Vehicle> Get(CancellationToken cancellationToken = default);

    List<Vehicle> GetByType(VehicleTypes vehicleType,
        CancellationToken cancellationToken = default);

    List<Vehicle> GetByManufacturer(string manufacturer,
        CancellationToken cancellationToken = default);

    List<Vehicle> GetByModel(string model,
        CancellationToken cancellationToken = default);
    
    List<Vehicle> GetByYear(int year,
        CancellationToken cancellationToken = default);
    
    Vehicle? GetByVin(string vin,
        CancellationToken cancellationToken = default);
}