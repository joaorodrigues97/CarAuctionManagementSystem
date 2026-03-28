using CarAuctionManagementSystem.Domain.Auctions;

namespace CarAuctionManagementSystem.Infrastructure.Repositories;

public sealed class AuctionRepository : IAuctionRepository
{
    private readonly List<Auction> _auctionListing = new();
    
    public bool Add(Auction auction, 
                    CancellationToken cancellationToken = default)
    {
        _auctionListing.Add(auction);

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

    public List<Auction> Get(CancellationToken cancellationToken = default)
    {
        return _auctionListing;
    }

    public Auction? GetByVin(string vin,
        CancellationToken cancellationToken = default)
    {
        return _auctionListing.FirstOrDefault(auction => auction.Vin == vin);
    }

    public bool IsActive(string vin,
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