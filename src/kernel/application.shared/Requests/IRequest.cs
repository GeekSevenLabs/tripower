// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IRequest;

public interface IRequest<out TResponse> : IRequest;