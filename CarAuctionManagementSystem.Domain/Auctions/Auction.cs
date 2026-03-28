namespace CarAuctionManagementSystem.Domain.Auctions;

public record Auction
{
    public string? Vin { get; set; }

    public int? Reserve { get; set; }

    public int CurrentBid { get; set; } = 0;

    public int LastBid { get; set; } = 0;

    public DateTime StartDate { get; set; } = DateTime.UtcNow;

    public DateTime? EndDate { get; set; }

    public bool IsAuctionActive { get; set; } = true;

    public bool? MeetReserve { get; set; } = null;
    
    public static Auction AddAuction(string vin, int? reserve)
    {
        return new Auction
        {
            Vin =  vin,
            Reserve = reserve
        };
    }
    
    public static void StopAuction(Auction auction)
    {
        auction.EndDate = DateTime.UtcNow;
        auction.IsAuctionActive = false;
        auction.LastBid = auction.CurrentBid;
        auction.MeetReserve = auction.LastBid > auction.Reserve;
    }
}