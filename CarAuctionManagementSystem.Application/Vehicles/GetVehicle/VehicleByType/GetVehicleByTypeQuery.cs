using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Vehicles;

namespace CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByType;

public record GetVehicleByTypeQuery(VehicleTypes VehicleType) : IQuery<List<Vehicle>>;