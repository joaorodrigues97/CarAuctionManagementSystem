using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Abstractions;
using CarAuctionManagementSystem.Domain.Vehicles;

namespace CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByManufacturer;

public sealed class GetVehicleByManufacturerQueryHandler(IVehicleRepository vehicleRepository) : IQueryHandler<GetVehicleByManufacturerQuery, List<Vehicle>>
{
    public Result<List<Vehicle>> Handle(GetVehicleByManufacturerQuery query, 
                                        CancellationToken cancellationToken)
    {
        List<Vehicle> vehiclesList = vehicleRepository.GetVehicleByManufacturer(query.Manufacturer);

        if (vehiclesList.Count > 0)
        {
            return Result.Success(vehiclesList);
        }

        return Result.Failure<List<Vehicle>>(VehicleErrors.NotFound);
    }
}