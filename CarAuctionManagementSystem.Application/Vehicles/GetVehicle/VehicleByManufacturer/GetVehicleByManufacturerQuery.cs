using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Vehicles;

namespace CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByManufacturer;

public record GetVehicleByManufacturerQuery(string Manufacturer) : IQuery<List<Vehicle>>;