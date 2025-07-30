using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

// ReSharper disable once CheckNamespace
namespace TriPower;

internal class HandlerRegistryEndpoint(IRequestProvider provider, IEndpointRouteBuilder builder) : IHandlerRegistry
{
    public IHandlerRegistry Register<THandler, TRequest>() where THandler : class, IHandler<TRequest> where TRequest : IRequest
    {
        var definition = (HandlerRequestDefinition<TRequest>)provider.GetRequiredDefinition<TRequest>();

        var endpoint = definition.Method switch
        {
            EndpointMethod.Post => builder.MapPost(definition.Path, (IHandlerMediator mediator, TRequest request) => mediator.SendAsync(request)),
            EndpointMethod.Get => builder.MapGet(definition.Path, (IHandlerMediator mediator, [AsParameters] TRequest request) => mediator.SendAsync(request)),
            EndpointMethod.Delete => builder.MapDelete(definition.Path, (IHandlerMediator mediator, [AsParameters] TRequest request) => mediator.SendAsync(request)),
            EndpointMethod.Put => builder.MapPut(definition.Path, (IHandlerMediator mediator, TRequest request) => mediator.SendAsync(request)),
            _ => throw new NotSupportedException($"Endpoint method {definition.Method} is not supported.")
        };

        endpoint.WithName(definition.Name);

        ConfigureSecurity(endpoint, definition);
        ConfigureValidation(endpoint, definition);
        
        return this;
    }

    public IHandlerRegistry Register<THandler, TRequest, TResponse>() 
        where THandler : class, IHandler<TRequest, TResponse> 
        where TRequest : IRequest, IRequest<TResponse>
        where TResponse : class
    {
        var definition = (HandlerRequestDefinition<TRequest, TResponse>)provider.GetRequiredDefinition<TRequest>();

        var endpoint = definition.Method switch
        {
            EndpointMethod.Post => builder.MapPost(definition.Path, (IHandlerMediator mediator, [AsParameters] TRequest request) => mediator.SendAsync<TRequest, TResponse>(request)),
            EndpointMethod.Get => builder.MapGet(definition.Path, (IHandlerMediator mediator, [AsParameters] TRequest request) => mediator.SendAsync<TRequest, TResponse>(request)),
            EndpointMethod.Delete => builder.MapDelete(definition.Path, (IHandlerMediator mediator, [AsParameters] TRequest request) => mediator.SendAsync<TRequest, TResponse>(request)),
            EndpointMethod.Put => builder.MapPut(definition.Path, (IHandlerMediator mediator, [AsParameters] TRequest request) => mediator.SendAsync<TRequest, TResponse>(request)),
            _ => throw new NotSupportedException($"Endpoint method {definition.Method} is not supported.")
        };

        endpoint.WithName(definition.Name);

        ConfigureSecurity(endpoint, definition);
        ConfigureValidation(endpoint, definition);
        
        return this;
    }

    private static void ConfigureSecurity<TRequest>(RouteHandlerBuilder endpoint, HandlerRequestDefinition<TRequest> definition) where TRequest : IRequest
    {
        switch (definition)
        {
            case { RequiredAuthentication: true, RequiredRoles.IsEmpty: true }:
                endpoint.RequireAuthorization();
                break;
            case { RequiredAuthentication: true, RequiredRoles.IsNotEmpty: true }:
                endpoint.RequireAuthorization(builder => { builder.RequireRole(definition.RequiredRoles); });
                break;
            default:
                endpoint.AllowAnonymous();
                break;
        }
    }
    
    private static void ConfigureValidation<TRequest>(RouteHandlerBuilder endpoint, HandlerRequestDefinition<TRequest> definition) where TRequest : IRequest
    {
        if (definition.RequiredValidation)
        {
            // TODO: Implementar validação a nível de endpoint
        }
    }
}