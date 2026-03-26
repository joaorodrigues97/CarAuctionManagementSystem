using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Auctions;

namespace CarAuctionManagementSystem.Application.Auctions.GetAuctions.AllAuctions;

public record GetAllAuctionsQuery() : IQuery<List<Auction>>;