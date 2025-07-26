using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

// ReSharper disable once CheckNamespace
namespace TriPower;

internal class HandlerRegistryEndpoint(IRequestProvider provider, IEndpointRouteBuilder builder) : IHandlerRegistry
{
    public IHandlerRegistry Register<THandler, TRequest>() where THandler : class, IHandler<TRequest> where TRequest : IRequest
    {
        var definition = provider.GetRequiredDefinition<TRequest>();

        var endpoint = definition.Method switch
        {
            EndpointMethod.Post => builder.MapPost(definition.Path, (IHandlerMediator mediator, TRequest request) => mediator.SendAsync(request)),
            EndpointMethod.Get => builder.MapGet(definition.Path, (IHandlerMediator mediator, [AsParameters] TRequest request) => mediator.SendAsync(request)),
            EndpointMethod.Delete => builder.MapDelete(definition.Path, (IHandlerMediator mediator, [AsParameters] TRequest request) => mediator.SendAsync(request)),
            _ => throw new NotSupportedException($"Endpoint method {definition.Method} is not supported.")
        };

        endpoint.WithName(typeof(THandler).Name);

        ConfigureSecurity(endpoint, definition);
        ConfigureValidation(endpoint, definition);
        
        return this;
    }

    public IHandlerRegistry Register<THandler, TRequest, TResponse>() where THandler : class, IHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        var definition = provider.GetRequiredDefinition<TRequest>();

        var endpoint = definition.Method switch
        {
            EndpointMethod.Post => builder.MapPost(definition.Path, (IHandlerMediator mediator, TRequest request) => mediator.SendAsync<TRequest, TResponse>(request)),
            EndpointMethod.Get => builder.MapGet(definition.Path, (IHandlerMediator mediator, [AsParameters] TRequest request) => mediator.SendAsync<TRequest, TResponse>(request)),
            EndpointMethod.Delete => builder.MapDelete(definition.Path, (IHandlerMediator mediator, [AsParameters] TRequest request) => mediator.SendAsync<TRequest, TResponse>(request)),
            _ => throw new NotSupportedException($"Endpoint method {definition.Method} is not supported.")
        };

        endpoint.WithName(typeof(THandler).Name);

        ConfigureSecurity(endpoint, definition);
        ConfigureValidation(endpoint, definition);
        
        return this;
    }

    private static void ConfigureSecurity(RouteHandlerBuilder endpoint, HandlerRequestDefinition definition)
    {
        if (definition.RequiredAuthentication)
        {
            endpoint.RequireAuthorization(b =>
            {
                b.RequireRole(definition.RequiredRoles);
                // TODO: Melhorar adição de claims requeridas
            });
        }
        else
        {
            endpoint.AllowAnonymous();
        }
    }
    
    private static void ConfigureValidation(RouteHandlerBuilder endpoint, HandlerRequestDefinition definition)
    {
        if (definition.RequiredValidation)
        {
            // TODO: Implementar validação a nível de endpoint
        }
    }
}