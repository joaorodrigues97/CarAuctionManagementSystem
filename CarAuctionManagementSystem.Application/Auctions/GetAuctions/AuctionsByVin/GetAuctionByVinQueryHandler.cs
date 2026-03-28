using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Abstractions;
using CarAuctionManagementSystem.Domain.Auctions;

namespace CarAuctionManagementSystem.Application.Auctions.GetAuctions.AuctionsByVin;

public class GetAuctionByVinQueryHandler(IAuctionRepository auctionRepository) : IQueryHandler<GetAuctionByVinQuery, Auction>
{
    public Result<Auction> Handle(GetAuctionByVinQuery query, 
                                   CancellationToken cancellationToken)
    {
        Auction? auction = auctionRepository.GetByVin(query.Vin);

        if (auction is not null)
        {
            return Result.Success(auction);
        }

        return Result.Failure<Auction>(AuctionErrors.NotFound);
    }
}