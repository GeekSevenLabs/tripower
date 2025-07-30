// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IHandlerRequestConfiguration<TRequest> where TRequest : IRequest
{
    public void OnConfigure(IHandlerRequestDefinitionBuilder<TRequest> builder);
}

public interface IHandlerRequestConfiguration<TRequest, TResponse> 
    where TRequest : IRequest, IRequest<TResponse>
    where TResponse : class
{
    public void OnConfigure(IHandlerRequestDefinitionBuilder<TRequest, TResponse> builder);
}