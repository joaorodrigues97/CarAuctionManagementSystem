using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Auctions;

namespace CarAuctionManagementSystem.Application.Auctions.GetAuctions.AuctionsByVin;

public record GetAuctionByVinQuery(string Vin) : IQuery<Auction>;