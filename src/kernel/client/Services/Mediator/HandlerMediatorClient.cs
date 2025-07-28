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
        
        await TrowWhenIsNotSuccessResponseAsync(response, cancellationToken);
    }

    public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>
    {
        var definition = provider.GetRequiredDefinition<TRequest>();
        
        var requestMessage = CreateRequest(definition, request);
        var response = await client.SendAsync(requestMessage, cancellationToken);

        await TrowWhenIsNotSuccessResponseAsync(response, cancellationToken);
        
        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<TResponse>(json)!;
    }

    private static HttpRequestMessage CreateRequest<TRequest>(IHandlerRequestDefinition definition, TRequest request) where TRequest : IRequest
    {
        // TODO: Implementar mecanismo para obter o SerializerContext
        return new HttpRequestMessage(IdentifyMethod(definition), BuildPath(definition, request))
        {
            Content = CreateContent(definition, request),
        };
    }
    
    private static async Task TrowWhenIsNotSuccessResponseAsync(HttpResponseMessage response, CancellationToken token)
    {
        if (response.IsSuccessStatusCode) return;

        var content = await response.Content.ReadAsStringAsync(token);
        throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {content}", null, response.StatusCode);
    }

    private static StringContent CreateContent<TRequest>(IHandlerRequestDefinition definition, TRequest request) where TRequest : IRequest
    {
        return definition.Method switch
        {
            EndpointMethod.Get => new StringContent(""), // GET requests typically do not have a body
            EndpointMethod.Post => new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"),
            EndpointMethod.Delete => new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"),
            _ => throw new NotSupportedException($"The method {definition.Method} is not supported.")
        };
    }
    
    private static HttpMethod IdentifyMethod(IHandlerRequestDefinition definition)
    {
        return definition.Method switch
        {
            EndpointMethod.Get => HttpMethod.Get,
            EndpointMethod.Post => HttpMethod.Post,
            EndpointMethod.Delete => HttpMethod.Delete,
            _ => throw new NotSupportedException($"The method {definition.Method} is not supported.")
        };
    }

    private static string BuildPath<TRequest>(IHandlerRequestDefinition definition, TRequest request) where TRequest : IRequest
    {
        return "/api"+((HandlerRequestDefinition<TRequest>)definition).BuildPath(request);
    }
}