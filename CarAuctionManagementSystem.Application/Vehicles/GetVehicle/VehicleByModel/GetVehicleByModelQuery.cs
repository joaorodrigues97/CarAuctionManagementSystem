using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Vehicles;

namespace CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByModel;

public record GetVehicleByModelQuery(string Model) : IQuery<List<Vehicle>>;