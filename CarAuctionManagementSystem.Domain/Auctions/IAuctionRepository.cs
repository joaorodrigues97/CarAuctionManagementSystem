namespace CarAuctionManagementSystem.Domain.Auctions;

public interface IAuctionRepository
{
    bool StartAuction(Auction auction,
                      CancellationToken cancellationToken = default);

    bool StopAuction(string vin,
                      CancellationToken cancellationToken = default);

    bool Bid(int bid,
            string vin,
            CancellationToken cancellationToken = default);

    List<Auction> GetAuctions(CancellationToken cancellationToken = default);

    Auction? GetAuctionByVin(string vin,
                             CancellationToken cancellationToken = default);

    bool IsAuctionActive(string vin,
                         CancellationToken cancellationToken = default);

    bool IsBidValid(string vin,
                    int bid,
                    CancellationToken cancellationToken = default);
}