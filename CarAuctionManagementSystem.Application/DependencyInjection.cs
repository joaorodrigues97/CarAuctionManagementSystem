using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Application.Auctions.Bid;
using CarAuctionManagementSystem.Application.Auctions.GetAuctions.AllAuctions;
using CarAuctionManagementSystem.Application.Auctions.GetAuctions.AuctionsByVin;
using CarAuctionManagementSystem.Application.Auctions.StartAuction;
using CarAuctionManagementSystem.Application.Auctions.StopAuction;
using CarAuctionManagementSystem.Application.Vehicles.AddVehicle;
using CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByManufacturer;
using CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByModel;
using CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByType;
using CarAuctionManagementSystem.Application.Vehicles.GetVehicle.VehicleByYear;
using CarAuctionManagementSystem.Domain.Auctions;
using CarAuctionManagementSystem.Domain.Vehicles;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CarAuctionManagementSystem.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        //Command & Queries : Vehicles
        services.AddScoped<ICommandHandler<AddVehicleCommand, bool>, AddVehicleCommandHandler>();
        services.AddScoped<IQueryHandler<GetVehicleByTypeQuery, List<Vehicle>>, GetVehicleByTypeQueryHandler>();
        services.AddScoped<IQueryHandler<GetVehicleByManufacturerQuery, List<Vehicle>>, GetVehicleByManufacturerQueryHandler>();
        services.AddScoped<IQueryHandler<GetVehicleByModelQuery, List<Vehicle>>, GetVehicleByModelQueryHandler>();
        services.AddScoped<IQueryHandler<GetVehicleByYearQuery, List<Vehicle>>, GetVehicleByYearQueryHandler>();
        //Command & Queries : Auctions
        services.AddScoped<IQueryHandler<GetAuctionByVinQuery, Auction>, GetAuctionByVinQueryHandler>();
        services.AddScoped<IQueryHandler<GetAllAuctionsQuery, List<Auction>>, GetAllAuctionsQueryHandler>();
        services.AddScoped<ICommandHandler<StartAuctionCommand, bool>, StartAuctionCommandHandler>();
        services.AddScoped<ICommandHandler<StopAuctionCommand, bool>, StopAuctionCommandHandler>();
        services.AddScoped<ICommandHandler<BidAuctionCommand, bool>, BidAuctionCommandHandler>();
        //Validators
        services.AddScoped<IValidator<AddVehicleCommand>, AddVehicleCommandValidator>();
        services.AddScoped<IValidator<StartAuctionCommand>, StartAuctionCommandValidator>();
        services.AddScoped<IValidator<StopAuctionCommand>, StopAuctionCommandValidator>();
        services.AddScoped<IValidator<BidAuctionCommand>, BidAuctionCommandValidator>();
    }
}