using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Vehicles;

namespace CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByYear;

public record GetVehicleByYearQuery(int Year) : IQuery<List<Vehicle>>;