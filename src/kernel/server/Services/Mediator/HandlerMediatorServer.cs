// ReSharper disable once CheckNamespace
namespace TriPower;

public class HandlerMediatorServer(IServiceProvider serviceProvider) : IHandlerMediator
{
    public async Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) 
        where TRequest : IRequest
    {
        var scope = serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<IHandler<TRequest>>();

        await handler.HandleAsync(request, cancellationToken);
    }

    public Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) 
        where TRequest : IRequest, IRequest<TResponse>
        where TResponse : class
    {
        var scope = serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<IHandler<TRequest, TResponse>>();

        return handler.HandleAsync(request, cancellationToken);
    }
}