using CarAuctionManagementSystem.Application.Abstractions.Mediator;

namespace CarAuctionManagementSystem.Application.Auctions.StartAuction;

public record StartAuctionCommand(string? Vin) : ICommand<bool>;