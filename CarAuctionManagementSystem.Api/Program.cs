using CarAuctionManagementSystem.Application;
using CarAuctionManagementSystem.Auctions;
using CarAuctionManagementSystem.Infrastructure;
using CarAuctionManagementSystem.Vehicles;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddQuartz();

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

builder.Services.AddApplication();

builder.Services.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapVehiclesEndpoints();
app.MapAuctionsEndpoints();

app.UseHttpsRedirection();

app.Run();
