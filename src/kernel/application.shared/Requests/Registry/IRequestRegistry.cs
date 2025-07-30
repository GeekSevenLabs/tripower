// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IRequestRegistry
{
    IRequestRegistry Register<TRequest>(Action<IHandlerRequestDefinitionBuilder<TRequest>> builder) 
        where TRequest : IRequest;
    IRequestRegistry Register<TRequest>(IHandlerRequestConfiguration<TRequest> configuration) 
        where TRequest : IRequest;
    
    IRequestRegistry Register<TRequest, TResponse>(Action<IHandlerRequestDefinitionBuilder<TRequest, TResponse>> builder) 
        where TRequest : IRequest, IRequest<TResponse> 
        where TResponse : class;
    IRequestRegistry Register<TRequest, TResponse>(IHandlerRequestConfiguration<TRequest, TResponse> configuration) 
        where TRequest : IRequest, IRequest<TResponse> 
        where TResponse : class;
}