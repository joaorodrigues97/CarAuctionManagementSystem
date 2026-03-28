using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Vehicles;

namespace CarAuctionManagementSystem.Application.Vehicles.GetVehicle.AllVehicles;

public record GetAllVehiclesQuery() : IQuery<List<Vehicle>>;