using CarAuctionManagementSystem.Domain.Abstractions;

namespace CarAuctionManagementSystem.Application.Abstractions.Mediator;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Result<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
}