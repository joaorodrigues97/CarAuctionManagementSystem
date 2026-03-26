using CarAuctionManagementSystem.Domain.Auctions;

namespace CarAuctionManagementSystem.Auctions;

public record StartAuctionRequest(string Vin,
                                  int StartingBid);