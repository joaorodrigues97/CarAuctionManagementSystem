using CarAuctionManagementSystem.Domain.Abstractions;

namespace CarAuctionManagementSystem.Domain.Vehicles;

public static class VehicleErrors
{
    public static readonly Error NotFound = new(
        "Vehicles.NotFound",
        "No vehicles were found!");
    
    public static readonly Error Conflict = new(
        "Vehicles.Conflict",
        "Vehicle already exists!");
}