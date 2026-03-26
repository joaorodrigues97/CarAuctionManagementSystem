using CarAuctionManagementSystem.Domain.Vehicles;
using FluentValidation;

namespace CarAuctionManagementSystem.Application.Vehicles.AddVehicle;

public class AddVehicleCommandValidator : AbstractValidator<AddVehicleCommand>
{
    public AddVehicleCommandValidator()
    {
        RuleFor(input => input.NumberOfDoors)
            .NotEmpty()
            .When(input => input.VehicleType == VehicleTypes.Hatchback || input.VehicleType == VehicleTypes.Sedan)
            .WithErrorCode("Vehicles.BadRequest")
            .WithMessage("Number of doors is a required field for Sedans or Hatchbacks!");
        
        RuleFor(input => input.NumberOfDoors)
            .Empty()
            .When(input => input.VehicleType == VehicleTypes.SUV || input.VehicleType == VehicleTypes.Truck)
            .WithErrorCode("Vehicles.BadRequest")
            .WithMessage("Number of doors is not a valid field for SUV's or Trucks!");
        
        RuleFor(input => input.NumberOfSeats)
            .NotEmpty()
            .When(input => input.VehicleType == VehicleTypes.SUV)
            .WithErrorCode("Vehicles.BadRequest")
            .WithMessage("Number of seats is a required field for SUV's!");
        
        RuleFor(input => input.NumberOfSeats)
            .Empty()
            .When(input => input.VehicleType == VehicleTypes.Hatchback 
                           || input.VehicleType == VehicleTypes.Sedan 
                           || input.VehicleType == VehicleTypes.Truck)
            .WithErrorCode("Vehicles.BadRequest")
            .WithMessage("Number of seats is not a valid field for Hatchbacks, Sedans or Trucks!");
        
        RuleFor(input => input.LoadCapacity)
            .NotEmpty()
            .When(input => input.VehicleType == VehicleTypes.Truck)
            .WithErrorCode("Vehicles.BadRequest")
            .WithMessage("Load capacity is a required field for Trucks!");
        
        RuleFor(input => input.LoadCapacity)
            .Empty()
            .When(input => input.VehicleType == VehicleTypes.Hatchback 
                           || input.VehicleType == VehicleTypes.Sedan 
                           || input.VehicleType == VehicleTypes.SUV)
            .WithErrorCode("Vehicles.BadRequest")
            .WithMessage("Load capacity is not a valid field for Hatchbacks, Sedans or SUV's!");

        RuleFor(input => input.Vin)
            .NotEmpty()
            .WithErrorCode("Vehicles.BadRequest")
            .WithMessage("VIN is a required field");
        
        RuleFor(input => input.Manufacturer)
            .NotEmpty()
            .WithErrorCode("Vehicles.BadRequest")
            .WithMessage("Manufacturer is a required field");
        
        RuleFor(input => input.Model)
            .NotEmpty()
            .WithErrorCode("Vehicles.BadRequest")
            .WithMessage("Model is a required field");
        
        RuleFor(input => input.Year)
            .NotEmpty()
            .WithErrorCode("Vehicles.BadRequest")
            .WithMessage("Year is a required field");
        
        RuleFor(input => input.StartingBid)
            .NotEmpty()
            .WithErrorCode("Vehicles.BadRequest")
            .WithMessage("Starting bid is a required field");
    }
}