using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Abstractions;
using CarAuctionManagementSystem.Domain.Vehicles;

namespace CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByType;

public sealed class GetVehicleByTypeQueryHandler(IVehicleRepository vehicleRepository) : IQueryHandler<GetVehicleByTypeQuery, List<Vehicle>>
{
    public Result<List<Vehicle>> Handle(GetVehicleByTypeQuery query, 
                                        CancellationToken cancellationToken)
    {
        List<Vehicle> vehiclesList = vehicleRepository.GetVehicleByType(query.VehicleType);
        
        if (vehiclesList.Count > 0)
        {
            return Result.Success(vehiclesList);
        }

        return Result.Failure<List<Vehicle>>(VehicleErrors.NotFound);
    }
}