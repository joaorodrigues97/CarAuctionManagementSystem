using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Abstractions;
using CarAuctionManagementSystem.Domain.Vehicles;

namespace CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByModel;

public sealed class GetVehicleByModelQueryHandler(IVehicleRepository vehicleRepository) : IQueryHandler<GetVehicleByModelQuery, List<Vehicle>>
{
    public Result<List<Vehicle>> Handle(GetVehicleByModelQuery query, 
                                        CancellationToken cancellationToken)
    {
        List<Vehicle> vehiclesList = vehicleRepository.GetByModel(query.Model);
        
        if (vehiclesList.Count > 0)
        {
            return Result.Success(vehiclesList);
        }

        return Result.Failure<List<Vehicle>>(VehicleErrors.NotFound);
    }
}