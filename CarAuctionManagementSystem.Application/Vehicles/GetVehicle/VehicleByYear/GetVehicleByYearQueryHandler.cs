using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Abstractions;
using CarAuctionManagementSystem.Domain.Vehicles;

namespace CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByYear;

public sealed class GetVehicleByYearQueryHandler(IVehicleRepository vehicleRepository) : IQueryHandler<GetVehicleByYearQuery, List<Vehicle>>
{
    public Result<List<Vehicle>> Handle(GetVehicleByYearQuery query, 
                                        CancellationToken cancellationToken)
    {
        List<Vehicle> vehiclesList = vehicleRepository.GetVehicleByYear(query.Year);
        
        if (vehiclesList.Count > 0)
        {
            return Result.Success(vehiclesList);
        }

        return Result.Failure<List<Vehicle>>(VehicleErrors.NotFound);
    }
}