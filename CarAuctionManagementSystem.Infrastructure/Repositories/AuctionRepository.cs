using CarAuctionManagementSystem.Domain.Auctions;

namespace CarAuctionManagementSystem.Infrastructure.Repositories;

public sealed class AuctionRepository : IAuctionRepository
{
    private readonly List<Auction> _auctionListing = new();
    
    public bool StartAuction(Auction auction, 
                             CancellationToken cancellationToken = default)
    {
        _auctionListing.Add(auction);

        return true;
    }

    public bool StopAuction(string vin,
                            CancellationToken cancellationToken = default)
    {
        Auction auction = _auctionListing.FirstOrDefault(auction => auction.Vin == vin)!;
        
        auction.EndDate = DateTime.UtcNow;
        auction.IsAuctionActive = false;
        auction.LastBid = auction.CurrentBid;
        auction.MeetReserve = auction.LastBid > auction.StartingBid;

        return true;
    }

    public bool Bid(int bid, 
                   string vin,
                   CancellationToken cancellationToken = default)
    {
        Auction auction = _auctionListing.FirstOrDefault(auction => auction.Vin == vin)!;

        auction.CurrentBid = bid;

        return true;
    }

    public List<Auction> GetAuctions(CancellationToken cancellationToken = default)
    {
        return _auctionListing;
    }

    public Auction? GetAuctionByVin(string vin, 
                                    CancellationToken cancellationToken = default)
    {
        return _auctionListing.FirstOrDefault(auction => auction.Vin == vin);
    }

    public bool IsAuctionActive(string vin, 
                                CancellationToken cancellationToken = default)
    {
        return _auctionListing.FirstOrDefault(auction => auction.Vin == vin)!.IsAuctionActive;
    }

    public bool IsBidValid(string vin, 
                           int bid, 
                           CancellationToken cancellationToken = default)
    {
        return _auctionListing.FirstOrDefault(auction => auction.Vin == vin)!.CurrentBid < bid;
    }
}