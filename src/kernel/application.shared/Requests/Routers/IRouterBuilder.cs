// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IRouterBuilder<TRequest> where TRequest : IRequest
{
    IRouterBuilder<TRequest> AddSegment(string segment);
    IRouterBuilder<TRequest> AddSegments(params string[] segment);
    IRouterBuilder<TRequest> AddParameter(Expression<Func<TRequest, object>> expression);
    IRouterBuilder<TRequest> AddQueryParameter(Expression<Func<TRequest, object>> expression);
    

    internal string BuildRoutePattern();
    internal Func<TRequest, string> BuildRouteGenerator();
}