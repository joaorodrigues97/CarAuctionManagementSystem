namespace CarAuctionManagementSystem.Domain.Auctions;

public interface IAuctionRepository
{
    bool Add(Auction auction,
             CancellationToken cancellationToken = default);

    bool Bid(int bid,
             string vin,
             CancellationToken cancellationToken = default);

    List<Auction> Get(CancellationToken cancellationToken = default);

    Auction? GetByVin(string vin,
        CancellationToken cancellationToken = default);

    bool IsActive(string vin,
        CancellationToken cancellationToken = default);

    bool IsBidValid(string vin,
                    int bid,
                    CancellationToken cancellationToken = default);
}