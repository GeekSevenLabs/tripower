using System.Text;
using System.Text.Json;

// ReSharper disable once CheckNamespace
namespace TriPower;

public class HandlerMediatorClient(IRequestProvider provider, HttpClient client) : IHandlerMediator
{
    public async Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        var definition = provider.GetRequiredDefinition<TRequest>();
        
        var requestMessage = CreateRequest(definition, request);
        var response = await client.SendAsync(requestMessage, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>
    {
        var definition = provider.GetRequiredDefinition<TRequest>();
        
        var requestMessage = CreateRequest(definition, request);
        var response = await client.SendAsync(requestMessage, cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<TResponse>(content)!;
    }

    private static HttpRequestMessage CreateRequest<TRequest>(HandlerRequestDefinition definition, TRequest request) where TRequest : IRequest
    {
        // TODO: Implementar mecanismo para obter o SerializerContext
        return new HttpRequestMessage(IdentifyMethod(definition), definition.BuildPath(request))
        {
            Content = CreateContent(definition, request),
        };
    }

    private static StringContent CreateContent<TRequest>(HandlerRequestDefinition definition, TRequest request) where TRequest : IRequest
    {
        return definition.Method switch
        {
            EndpointMethod.Get => new StringContent(""), // GET requests typically do not have a body
            EndpointMethod.Post => new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"),
            EndpointMethod.Delete => new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"),
            _ => throw new NotSupportedException($"The method {definition.Method} is not supported.")
        };
    }
    
    private static HttpMethod IdentifyMethod(HandlerRequestDefinition definition)
    {
        return definition.Method switch
        {
            EndpointMethod.Get => HttpMethod.Get,
            EndpointMethod.Post => HttpMethod.Post,
            EndpointMethod.Delete => HttpMethod.Delete,
            _ => throw new NotSupportedException($"The method {definition.Method} is not supported.")
        };
    }
}