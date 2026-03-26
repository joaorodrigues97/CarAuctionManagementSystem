using CarAuctionManagementSystem.Domain.Abstractions;

namespace CarAuctionManagementSystem.Domain.Auctions;

public static class AuctionErrors
{
    public static readonly Error VehicleNotExistsConflict = new(
        "Auctions.Conflict",
        "Vehicle does not exists!");
    
    public static readonly Error NotFound = new(
        "Auctions.NotFound",
        "No auctions were found!");
    
    public static readonly Error AuctionConflict = new(
        "Auctions.Conflict",
        "There's already an active auction for this vehicle!");
    
    public static readonly Error AuctionActive = new(
        "Auctions.BadRequest",
        "Auction is no longer active!");
    
    public static readonly Error BidValid = new(
        "Auctions.BadRequest",
        "The bid you are trying to make is not valid!");
}