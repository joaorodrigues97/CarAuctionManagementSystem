using CarAuctionManagementSystem.Domain.Auctions;
using CarAuctionManagementSystem.Domain.Vehicles;
using CarAuctionManagementSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CarAuctionManagementSystem.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IVehicleRepository, VehicleRepository>();
        services.AddSingleton<IAuctionRepository, AuctionRepository>();
    }
}