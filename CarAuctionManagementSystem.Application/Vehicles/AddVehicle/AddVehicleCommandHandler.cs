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

        var vehicleByVin = vehicleRepository.GetVehicleByVin(command.Vin!);

        if (vehicleByVin is not null)
        {
            return Result.Failure<bool>([VehicleErrors.Conflict]);
        }
        
        Vehicle vehicle = new Vehicle
        {
            VehicleType = command.VehicleType,
            Manufacturer = command.Manufacturer,
            Model = command.Model,
            Vin = command.Vin,
            Year = command.Year,
            LoadCapacity = command.LoadCapacity,
            StartingBid = command.StartingBid,
            NumberOfDoors = command.NumberOfDoors,
            NumberOfSeats = command.NumberOfSeats
        };
        
        var result = vehicleRepository.AddVehicle(vehicle);
        
        return Result.Success(result);
    }
}