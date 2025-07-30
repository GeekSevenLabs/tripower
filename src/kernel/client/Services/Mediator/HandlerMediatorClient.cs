using System.Text;
using System.Text.Json;

// ReSharper disable once CheckNamespace
namespace TriPower;

public class HandlerMediatorClient(IRequestProvider provider, HttpClient client) : IHandlerMediator
{
    public async Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        var definition = (HandlerRequestDefinition<TRequest>)provider.GetRequiredDefinition<TRequest>();
        
        var requestMessage = CreateRequest(definition, request);
        var response = await client.SendAsync(requestMessage, cancellationToken);
        
        await TrowWhenIsNotSuccessResponseAsync(response, cancellationToken);
    }

    public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) 
        where TRequest : IRequest, IRequest<TResponse>
        where TResponse : class
    {
        var definition = (HandlerRequestDefinition<TRequest, TResponse>)provider.GetRequiredDefinition<TRequest>();
        
        var requestMessage = CreateRequest(definition, request);
        var response = await client.SendAsync(requestMessage, cancellationToken);

        await TrowWhenIsNotSuccessResponseAsync(response, cancellationToken);
        
        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize(json, definition.ResponseTypeInfo)!;
    }

    private static HttpRequestMessage CreateRequest<TRequest>(HandlerRequestDefinition<TRequest> definition, TRequest request) where TRequest : IRequest
    {
        return new HttpRequestMessage(
            IdentifyMethod(definition), 
            requestUri: BuildPath(definition, request))
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

    private static StringContent CreateContent<TRequest>(HandlerRequestDefinition<TRequest> definition, TRequest request) where TRequest : IRequest
    {
        return definition.Method switch
        {
            EndpointMethod.Get => new StringContent(""), // GET requests typically do not have a body
            EndpointMethod.Post => new StringContent(JsonSerializer.Serialize(request, definition.RequestTypeInfo), Encoding.UTF8, "application/json"),
            EndpointMethod.Delete => new StringContent(JsonSerializer.Serialize(request, definition.RequestTypeInfo), Encoding.UTF8, "application/json"),
            EndpointMethod.Put => new StringContent(JsonSerializer.Serialize(request, definition.RequestTypeInfo), Encoding.UTF8, "application/json"),
            _ => throw new NotSupportedException($"The method {definition.Method} is not supported.")
        };
    }
    
    private static HttpMethod IdentifyMethod<TRequest>(HandlerRequestDefinition<TRequest> definition) where TRequest : IRequest
    {
        return definition.Method switch
        {
            EndpointMethod.Get => HttpMethod.Get,
            EndpointMethod.Post => HttpMethod.Post,
            EndpointMethod.Delete => HttpMethod.Delete,
            EndpointMethod.Put => HttpMethod.Put,
            _ => throw new NotSupportedException($"The method {definition.Method} is not supported.")
        };
    }

    private static string BuildPath<TRequest>(IHandlerRequestDefinition definition, TRequest request) where TRequest : IRequest
    {
        // TODO: pensar em um mecanismo para deixar o segmento base do endpoint configurável
        return "/api/"+((HandlerRequestDefinition<TRequest>)definition).BuildPath(request);
    }
}