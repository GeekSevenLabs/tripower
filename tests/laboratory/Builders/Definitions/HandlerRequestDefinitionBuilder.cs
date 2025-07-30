using System.Text.Json.Serialization.Metadata;
using FluentValidation;
using Menso.Tools.Exceptions;

// ReSharper disable once CheckNamespace
namespace TriPower;

internal abstract class HandlerRequestDefinitionBuilderBase<TChild, TRequest> :
    IHandlerRequestDefinitionBuilderBase<TChild, TRequest>
    where TRequest : IRequest 
    where TChild : class, IHandlerRequestDefinitionBuilderBase<TChild, TRequest>
{

    protected HandlerRequestDefinitionBuilderBase(IHandlerRequestDefinition definition)
    {
        Throw.When.Null(definition, "Definition cannot be null.");
        Definition = definition;
    }
    
    private TChild Child => this as TChild ?? throw new InvalidOperationException("The child type is not correctly set. (HandlerRequestDefinitionBuilderBase)");
    private IHandlerRequestDefinition Definition { get; }

    public TChild WithName(string name)
    {
        Definition.ChangeName(name);
        return Child;
    }

    public TChild AllowAnonymous()
    {
        Cast<ISecurityDefinition>().ChangeAuthentication(false, requiredRoles: [], requiredClaims: []);
        return Child;
    }

    public TChild RequireAuthorization()
    {
        Cast<ISecurityDefinition>().ChangeAuthentication(true, requiredRoles: [], requiredClaims: []);
        return Child;
    }

    public TChild WithValidator<TValidator>() where TValidator : IValidator<TRequest>
    {
        Cast<IValidationDefinition<TRequest>>().ChangeValidator<TValidator>();
        return Child;
    }

    public TChild MapGet(Action<IRouterBuilder<TRequest>> routeBuilder)
    {
        return ChangeEndpointDefinition(EndpointMethod.Get, routeBuilder);
    }

    public TChild MapPost(Action<IRouterBuilder<TRequest>> routeBuilder)
    {
        return ChangeEndpointDefinition(EndpointMethod.Post, routeBuilder);
    }

    public TChild MapPut(Action<IRouterBuilder<TRequest>> routeBuilder)
    {
        return ChangeEndpointDefinition(EndpointMethod.Put, routeBuilder);
    }

    public TChild MapDelete(Action<IRouterBuilder<TRequest>> routeBuilder)
    {
        return ChangeEndpointDefinition(EndpointMethod.Delete, routeBuilder);
    }

    public TChild WithRequestTypeInfo(JsonTypeInfo<TRequest> typeInfo)
    {
        Cast<ISerializationDefinition<TRequest>>().ChangeRequestTypeInfo(typeInfo);
        return Child;
    }
    
    private TChild ChangeEndpointDefinition(EndpointMethod method, Action<IRouterBuilder<TRequest>> routeBuilder)
    {
        var builder = RouteBuilder<TRequest>.Empty;
        routeBuilder(builder);
        Cast<IEndpointDefinition<TRequest>>().ChangeEndpoint(builder.BuildRoutePattern(), method, builder.BuildRouteGenerator());
        return Child;
    }

    protected TDefinitionFragment Cast<TDefinitionFragment>()
    {
        return Definition is TDefinitionFragment fragment ? 
            fragment :
            throw new InvalidCastException($"The definition cannot be cast to {typeof(TDefinitionFragment).Name}.");
    }
    

    public IHandlerRequestDefinition Build() => Definition;

}

internal class HandlerRequestDefinitionBuilder<TRequest>() : 
    HandlerRequestDefinitionBuilderBase<IHandlerRequestDefinitionBuilder<TRequest>, TRequest>(new HandlerRequestDefinition<TRequest>()),
    IHandlerRequestDefinitionBuilder<TRequest>
    where TRequest : IRequest
{
    public static HandlerRequestDefinitionBuilder<TRequest> Empty => new();
}

internal class HandlerRequestDefinitionBuilder<TRequest, TResponse>() : 
    HandlerRequestDefinitionBuilderBase<IHandlerRequestDefinitionBuilder<TRequest, TResponse>, TRequest>(new HandlerRequestDefinition<TRequest, TResponse>()),
    IHandlerRequestDefinitionBuilder<TRequest, TResponse>
    where TRequest : IRequest, IRequest<TResponse> 
    where TResponse : class
{

    public static HandlerRequestDefinitionBuilder<TRequest, TResponse> Empty => new();


    public IHandlerRequestDefinitionBuilder<TRequest, TResponse> WithResponseTypeInfo(JsonTypeInfo<TResponse> typeInfo)
    {
        Cast<ISerializationDefinition<TRequest, TResponse>>().ChangeResponseTypeInfo(typeInfo);
        return this;
    }
}