// ReSharper disable once CheckNamespace
namespace TriPower;

internal class HandlerRegistryService(IServiceCollection services) : IHandlerRegistry
{
    public IHandlerRegistry Register<THandler, TRequest>() 
        where THandler : class, IHandler<TRequest>
        where TRequest : IRequest
    {
        services.AddTransient<IHandler<TRequest>, THandler>();
        return this;
    }

    public IHandlerRegistry Register<THandler, TRequest, TResponse>()
        where THandler : class, IHandler<TRequest, TResponse>
        where TRequest : IRequest, IRequest<TResponse>
        where TResponse : class

    {
        services.AddTransient<IHandler<TRequest, TResponse>, THandler>();
        return this;
    }
}