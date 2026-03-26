using CarAuctionManagementSystem.Application.Abstractions.Mediator;

namespace CarAuctionManagementSystem.Application.Auctions.StopAuction;

public record StopAuctionCommand(string Vin) : ICommand<bool>;