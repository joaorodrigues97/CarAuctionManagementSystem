using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Vehicles;

namespace CarAuctionManagementSystem.Application.Vehicles.AddVehicle;

public record AddVehicleCommand(VehicleTypes VehicleType,
                                int? NumberOfDoors,
                                int? NumberOfSeats,
                                int? LoadCapacity,
                                string? Vin,
                                string? Manufacturer,
                                string? Model,
                                int? Year,
                                int? StartingBid) : ICommand<bool>;