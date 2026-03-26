using CarAuctionManagementSystem.Domain.Vehicles;

namespace CarAuctionManagementSystem.Infrastructure.Repositories;

public sealed class VehicleRepository : IVehicleRepository
{
    private readonly List<Vehicle> _vehicleListing = new();
    
    public bool AddVehicle(Vehicle vehicle, 
                           CancellationToken cancellationToken = default)
    {
        _vehicleListing.Add(vehicle);

        return true;
    }

    public List<Vehicle> GetVehicleByType(VehicleTypes vehicleType, 
                                          CancellationToken cancellationToken = default)
    {
        return _vehicleListing.
            Where(vehicle => vehicle.VehicleType == vehicleType).
            ToList();
    }

    public List<Vehicle> GetVehicleByManufacturer(string manufacturer, 
                                                  CancellationToken cancellationToken = default)
    {
        return _vehicleListing.
            Where(vehicle => vehicle.Manufacturer == manufacturer).
            ToList();
    }

    public List<Vehicle> GetVehicleByModel(string model, 
                                           CancellationToken cancellationToken = default)
    {
        return _vehicleListing.
            Where(vehicle => vehicle.Model == model).
            ToList();
    }

    public List<Vehicle> GetVehicleByYear(int year, 
                                          CancellationToken cancellationToken = default)
    {
        return _vehicleListing.
            Where(vehicle => vehicle.Year == year).
            ToList();
    }

    public Vehicle? GetVehicleByVin(string vin, 
                                    CancellationToken cancellationToken = default)
    {
        return _vehicleListing.FirstOrDefault(vehicle => vehicle.Vin == vin);
    }
}