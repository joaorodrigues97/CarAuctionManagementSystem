using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Application.Auctions.Bid;
using CarAuctionManagementSystem.Application.Auctions.GetAuctions.AllAuctions;
using CarAuctionManagementSystem.Application.Auctions.GetAuctions.AuctionsByVin;
using CarAuctionManagementSystem.Application.Auctions.StartAuction;
using CarAuctionManagementSystem.Application.Auctions.StopAuction;
using CarAuctionManagementSystem.Application.Vehicles.AddVehicle;
using CarAuctionManagementSystem.Domain.Auctions;

namespace CarAuctionManagementSystem.Auctions;

public static class AuctionsEndpoints
{
    public static void MapAuctionsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auctions");

        group.MapPost("/start", StartAuction);
        group.MapPost("/stop", StopAuction);
        group.MapGet("/vin/{vin}", GetAuctionByVin);
        group.MapGet("", GetAllAuctions);
        group.MapPost("/bid", BidAuction);
    }
    
    private static IResult StartAuction(StartAuctionRequest request,
                                       ICommandHandler<StartAuctionCommand, bool> handler,
                                       CancellationToken cancellationToken)
    {
        StartAuctionCommand command = new StartAuctionCommand(request.Vin);

        var result = handler.Handle(command, cancellationToken);

        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
    }

    private static IResult StopAuction(StopAuctionRequest request,
                                      ICommandHandler<StopAuctionCommand, bool> handler,
                                      CancellationToken cancellationToken)
    {
        StopAuctionCommand command = new StopAuctionCommand(request.Vin);

        var result = handler.Handle(command, cancellationToken);
        
        return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Errors);
    }
    
    private static IResult GetAuctionByVin(string vin,
                                          IQueryHandler<GetAuctionByVinQuery, Auction> handler,
                                          CancellationToken cancellationToken)
    {
        GetAuctionByVinQuery query = new GetAuctionByVinQuery(vin);

        var result = handler.Handle(query, cancellationToken);

        return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Errors);
    }

    private static IResult GetAllAuctions(IQueryHandler<GetAllAuctionsQuery, List<Auction>> handler,
                                         CancellationToken cancellationToken)
    {
        GetAllAuctionsQuery query = new GetAllAuctionsQuery();

        var result = handler.Handle(query, cancellationToken);
        
        return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Errors);
    }
    
    private static IResult BidAuction(BidAuctionRequest request,
                                     ICommandHandler<BidAuctionCommand, bool> handler,
                                     CancellationToken cancellationToken)
    {
        BidAuctionCommand command = new BidAuctionCommand(request.Bid, 
                                                          request.Vin);

        var result = handler.Handle(command, cancellationToken);
        
        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
    }
}