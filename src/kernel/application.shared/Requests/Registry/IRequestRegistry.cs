// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IRequestRegistry
{
    IRequestRegistry Register<TRequest>(Action<IHandlerRequestDefinitionBuilder<TRequest>> builder) 
        where TRequest : IRequest;
    IRequestRegistry RegisterWithValidator<TRequest, TValidator>(Action<IHandlerRequestDefinitionBuilder<TRequest>> builder)
        where TRequest : IRequest 
        where TValidator : class, IValidator<TRequest>;
    
    IRequestRegistry Register<TRequest, TResponse>(Action<IHandlerRequestDefinitionBuilder<TRequest>> builder) 
        where TRequest : IRequest<TResponse>;
    IRequestRegistry RegisterWithValidator<TRequest, TResponse, TValidator>(Action<IHandlerRequestDefinitionBuilder<TRequest>> builder) 
        where TRequest : IRequest<TResponse> 
        where TValidator : class, IValidator<TRequest>;
}