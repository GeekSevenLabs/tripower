// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IRequestProvider
{
    IHandlerRequestDefinition GetRequiredDefinition<TRequest>() where TRequest : IRequest;
    
    internal void AddDefinition<TRequest>(IHandlerRequestDefinition definition) where TRequest : IRequest;
}