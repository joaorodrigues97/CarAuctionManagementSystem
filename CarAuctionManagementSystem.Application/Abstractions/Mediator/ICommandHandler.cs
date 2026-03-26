using CarAuctionManagementSystem.Domain.Abstractions;
using Microsoft.AspNetCore.Http;

namespace CarAuctionManagementSystem.Application.Abstractions.Mediator;

public interface ICommandHandler<in TCommand>
{
    Result Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResponse> 
    where TCommand : ICommand<TResponse>
{
    Result<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
}