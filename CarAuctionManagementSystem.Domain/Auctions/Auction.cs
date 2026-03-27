namespace CarAuctionManagementSystem.Domain.Auctions;

public record Auction
{
    public string? Vin { get; set; }

    public int? StartingBid { get; set; }

    public int CurrentBid { get; set; } = 0;

    public int LastBid { get; set; } = 0;

    public DateTime StartDate { get; set; } = DateTime.UtcNow;

    public DateTime? EndDate { get; set; }

    public bool IsAuctionActive { get; set; } = true;

    public bool? MeetReserve { get; set; } = null;
}