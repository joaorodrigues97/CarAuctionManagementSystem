using CarAuctionManagementSystem.Domain.Vehicles;

namespace CarAuctionManagementSystem.Infrastructure.Repositories;

public sealed class VehicleRepository : IVehicleRepository
{
    private readonly List<Vehicle> _vehicleListing = new();
    
    public bool Add(Vehicle vehicle,
        CancellationToken cancellationToken = default)
    {
        _vehicleListing.Add(vehicle);

        return true;
    }

    public List<Vehicle> Get(CancellationToken cancellationToken = default)
    {
        return _vehicleListing;
    }

    public List<Vehicle> GetByType(VehicleTypes vehicleType,
        CancellationToken cancellationToken = default)
    {
        return _vehicleListing.
            Where(vehicle => vehicle.VehicleType == vehicleType).
            ToList();
    }

    public List<Vehicle> GetByManufacturer(string manufacturer,
        CancellationToken cancellationToken = default)
    {
        return _vehicleListing.
            Where(vehicle => vehicle.Manufacturer == manufacturer).
            ToList();
    }

    public List<Vehicle> GetByModel(string model,
        CancellationToken cancellationToken = default)
    {
        return _vehicleListing.
            Where(vehicle => vehicle.Model == model).
            ToList();
    }

    public List<Vehicle> GetByYear(int year,
        CancellationToken cancellationToken = default)
    {
        return _vehicleListing.
            Where(vehicle => vehicle.Year == year).
            ToList();
    }

    public Vehicle? GetByVin(string vin,
        CancellationToken cancellationToken = default)
    {
        return _vehicleListing.FirstOrDefault(vehicle => vehicle.Vin == vin);
    }
}