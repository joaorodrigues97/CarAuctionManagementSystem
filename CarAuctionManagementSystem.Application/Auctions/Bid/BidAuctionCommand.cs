using CarAuctionManagementSystem.Application.Abstractions.Mediator;

namespace CarAuctionManagementSystem.Application.Auctions.Bid;

public record BidAuctionCommand(int Bid,
                                string Vin) : ICommand<bool>;