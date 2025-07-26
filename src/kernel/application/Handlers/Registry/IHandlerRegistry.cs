// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IHandlerRegistry
{
    IHandlerRegistry Register<THandler, TRequest>() where THandler : class, IHandler<TRequest> where TRequest : IRequest;
    IHandlerRegistry Register<THandler, TRequest, TResponse>() where THandler : class, IHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>;
}