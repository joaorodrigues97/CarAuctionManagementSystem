using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Abstractions;
using CarAuctionManagementSystem.Domain.Vehicles;

namespace CarAuctionManagementSystem.Application.Vehicles.GetVehicle.AllVehicles;

public sealed class GetAllVehiclesQueryHandler(IVehicleRepository vehicleRepository)
    : IQueryHandler<GetAllVehiclesQuery, List<Vehicle>>
{
    public Result<List<Vehicle>> Handle(GetAllVehiclesQuery query, CancellationToken cancellationToken)
    {
        List<Vehicle> vehiclesList = vehicleRepository.Get();
        
        if (vehiclesList.Count > 0)
        {
            return Result.Success(vehiclesList);
        }

        return Result.Failure<List<Vehicle>>(VehicleErrors.NotFound);
    }
}