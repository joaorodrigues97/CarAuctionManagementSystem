namespace CarAuctionManagementSystem.Domain.Vehicles;

public interface IVehicleRepository
{
    bool AddVehicle(Vehicle vehicle, 
                               CancellationToken cancellationToken = default);

    List<Vehicle> GetVehicleByType(VehicleTypes vehicleType,
                                   CancellationToken cancellationToken = default);

    List<Vehicle> GetVehicleByManufacturer(string manufacturer,
                                           CancellationToken cancellationToken = default);

    List<Vehicle> GetVehicleByModel(string model,
                                    CancellationToken cancellationToken = default);
    
    List<Vehicle> GetVehicleByYear(int year, 
                                   CancellationToken cancellationToken = default);
    
    Vehicle? GetVehicleByVin(string vin, 
                             CancellationToken cancellationToken = default);
}