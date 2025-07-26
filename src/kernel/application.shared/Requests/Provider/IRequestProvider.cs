// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IRequestProvider
{
    HandlerRequestDefinition GetRequiredDefinition<TRequest>() where TRequest : IRequest;
    
    internal void AddDefinition<TRequest>(HandlerRequestDefinition definition) where TRequest : IRequest;
}