using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Application.Abstractions.Utils;
using CarAuctionManagementSystem.Domain.Abstractions;
using CarAuctionManagementSystem.Domain.Vehicles;
using FluentValidation;

namespace CarAuctionManagementSystem.Application.Vehicles.AddVehicle;

public sealed class AddVehicleCommandHandler(IVehicleRepository vehicleRepository,
                                             IValidator<AddVehicleCommand> validator) : ICommandHandler<AddVehicleCommand, bool>
{
    public Result<bool> Handle(AddVehicleCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            return Result.Failure<bool>(validationResult.Errors.ConvertToError());
        }

        var vehicleByVin = vehicleRepository.GetByVin(command.Vin!);

        if (vehicleByVin is not null)
        {
            return Result.Failure<bool>([VehicleErrors.Conflict]);
        }

        Vehicle vehicle = 
            Vehicle.AddVehicle(
                command.VehicleType,
                command.NumberOfDoors,
                command.NumberOfSeats,
                command.LoadCapacity,
                command.Vin,
                command.Manufacturer,
                command.Model,
                command.Year,
                command.Reserve);
        
        var result = vehicleRepository.Add(vehicle);
        
        return Result.Success(result);
    }
}