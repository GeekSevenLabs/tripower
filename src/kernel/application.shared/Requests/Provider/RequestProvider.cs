// ReSharper disable once CheckNamespace
namespace TriPower;

internal class RequestProvider : IRequestProvider
{
    private readonly Dictionary<Type, HandlerRequestDefinition> _definitions = [];
    
    public static RequestProvider Empty => new RequestProvider();
    
    public HandlerRequestDefinition GetRequiredDefinition<TRequest>() where TRequest : IRequest
    {
        var type = typeof(TRequest);
        
        return _definitions.TryGetValue(type, out var definition) ? 
            definition : 
            throw new InvalidOperationException($"No request definition found for type {type.FullName}");
    }

    public void AddDefinition<TRequest>(HandlerRequestDefinition definition) where TRequest : IRequest
    {
        _definitions[typeof(TRequest)] = definition;
    }
}