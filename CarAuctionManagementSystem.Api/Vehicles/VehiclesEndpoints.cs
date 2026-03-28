using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Application.Vehicles.AddVehicle;
using CarAuctionManagementSystem.Application.Vehicles.GetVehicle.AllVehicles;
using CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByManufacturer;
using CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByModel;
using CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByType;
using CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByYear;
using CarAuctionManagementSystem.Domain.Abstractions;
using CarAuctionManagementSystem.Domain.Vehicles;

namespace CarAuctionManagementSystem.Vehicles;

public static class VehiclesEndpoints
{
    public static void MapVehiclesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/vehicles");

        group.MapPost("", AddVehicle);
        group.MapGet("", GetAllVehicles);
        group.MapGet("/types/{vehicleType}", GetVehicleByType);
        group.MapGet("/manufacturers/{manufacturer}", GetVehicleByManufacturer);
        group.MapGet("/models/{model}", GetVehicleByModel);
        group.MapGet("/years/{year}", GetVehicleByYear);
    }

    private static IResult AddVehicle(AddVehicleRequest request,
                                      ICommandHandler<AddVehicleCommand, bool> handler,
                                      CancellationToken cancellationToken)
    {
        var command = new AddVehicleCommand(request.VehicleType,
            request.NumberOfDoors,
            request.NumberOfSeats,
            request.LoadCapacity,
            request.Vin,
            request.Manufacturer,
            request.Model,
            request.Year,
            request.Reserve);

        var result = handler.Handle(command, cancellationToken);

        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
    }

    private static IResult GetVehicleByType(VehicleTypes vehicleType,
                                           IQueryHandler<GetVehicleByTypeQuery, List<Vehicle>> handler,
                                           CancellationToken cancellationToken)
    {
        GetVehicleByTypeQuery query = new GetVehicleByTypeQuery(vehicleType);

        var result = handler.Handle(query, cancellationToken);

        return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Errors);
    }
    
    private static IResult GetVehicleByManufacturer(string manufacturer,
                                                   IQueryHandler<GetVehicleByManufacturerQuery, List<Vehicle>> handler, 
                                                   CancellationToken cancellationToken)
    {
        GetVehicleByManufacturerQuery query = new GetVehicleByManufacturerQuery(manufacturer);

        var result = handler.Handle(query, cancellationToken);

        return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Errors);
    }

    private static IResult GetVehicleByModel(string model,
                                            IQueryHandler<GetVehicleByModelQuery, List<Vehicle>> handler,
                                            CancellationToken cancellationToken)
    {
        GetVehicleByModelQuery query = new GetVehicleByModelQuery(model);

        var result = handler.Handle(query, cancellationToken);
        
        return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Errors);
    }
    
    private static IResult GetVehicleByYear(int year, 
                                           IQueryHandler<GetVehicleByYearQuery, List<Vehicle>> handler,
                                           CancellationToken cancellationToken)
    {
        GetVehicleByYearQuery query = new GetVehicleByYearQuery(year);

        var result = handler.Handle(query, cancellationToken);
        
        return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Errors);
    }
    
    private static IResult GetAllVehicles(IQueryHandler<GetAllVehiclesQuery, List<Vehicle>> handler,
                                         CancellationToken cancellationToken)
    {
        GetAllVehiclesQuery query = new GetAllVehiclesQuery();

        var result = handler.Handle(query, cancellationToken);
        
        return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Errors);
    }
}