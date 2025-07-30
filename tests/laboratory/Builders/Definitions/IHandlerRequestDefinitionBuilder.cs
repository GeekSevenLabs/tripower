using System.Text.Json.Serialization.Metadata;
using FluentValidation;

// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IHandlerRequestDefinitionBuilderBase<out TChild, TRequest> where TRequest : IRequest
{
    TChild WithName(string name);


    #region Security

    TChild AllowAnonymous();
    TChild RequireAuthorization();

    #endregion

    #region Validation

    TChild WithValidator<TValidator>() where TValidator : IValidator<TRequest>;

    #endregion

    #region Endpoint

    TChild MapGet(Action<IRouterBuilder<TRequest>> routeBuilder);
    TChild MapPost(Action<IRouterBuilder<TRequest>> routeBuilder);
    TChild MapPut(Action<IRouterBuilder<TRequest>> routeBuilder);
    TChild MapDelete(Action<IRouterBuilder<TRequest>> routeBuilder);

    #endregion

    #region Serialization

    TChild WithRequestTypeInfo(JsonTypeInfo<TRequest> typeInfo);

    #endregion
    
    internal IHandlerRequestDefinition Build();
}

public interface IHandlerRequestDefinitionBuilder<TRequest> :
    IHandlerRequestDefinitionBuilderBase<IHandlerRequestDefinitionBuilder<TRequest>, TRequest>
    where TRequest : IRequest;

public interface IHandlerRequestDefinitionBuilder<TRequest, TResponse> :
    IHandlerRequestDefinitionBuilderBase<IHandlerRequestDefinitionBuilder<TRequest, TResponse>, TRequest>
    where TRequest : IRequest, IRequest<TResponse> 
    where TResponse : class
{
    IHandlerRequestDefinitionBuilder<TRequest, TResponse> WithResponseTypeInfo(JsonTypeInfo<TResponse> typeInfo);
}
