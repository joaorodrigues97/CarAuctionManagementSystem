using CarAuctionManagementSystem.Application.Abstractions.Mediator;
using CarAuctionManagementSystem.Domain.Abstractions;
using CarAuctionManagementSystem.Domain.Auctions;

namespace CarAuctionManagementSystem.Application.Auctions.GetAuctions.AllAuctions;

public sealed class GetAllAuctionsQueryHandler(IAuctionRepository auctionRepository) : IQueryHandler<GetAllAuctionsQuery, List<Auction>>
{
    public Result<List<Auction>> Handle(GetAllAuctionsQuery query, CancellationToken cancellationToken)
    {
        List<Auction>? auctionsList = auctionRepository.Get();

        if (auctionsList.Count > 0)
        {
            return Result.Success(auctionsList);
        }

        return Result.Failure<List<Auction>>(AuctionErrors.NotFound);
    }
}