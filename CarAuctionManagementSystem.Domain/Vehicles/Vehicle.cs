using System.Diagnostics;
using System.Text.Json.Serialization;

namespace CarAuctionManagementSystem.Domain.Vehicles;

public record Vehicle
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VehicleTypes VehicleType { get; set; }

    public int? NumberOfDoors { get; set; }

    public int? NumberOfSeats { get; set; }

    public int? LoadCapacity { get; set; }

    public string? Vin { get; set; }

    public string? Manufacturer { get; set; }

    public string? Model { get; set; }

    public int? Year { get; set; }

    public int? StartingBid { get; set; }
}