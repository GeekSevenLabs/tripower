// ReSharper disable once CheckNamespace
namespace TriPower;

public class HandlerMediatorServer(IRequestProvider requestProvider, IServiceProvider serviceProvider) : IHandlerMediator
{
    public async Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        var scope = serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<IHandler<TRequest>>();

        await handler.HandleAsync(request, cancellationToken);
    }

    public Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>
    {
        var scope = serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<IHandler<TRequest, TResponse>>();

        return handler.HandleAsync(request, cancellationToken);
    }
}