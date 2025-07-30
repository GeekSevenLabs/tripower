using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace TriPower;

internal class RouteBuilder<TRequest> : IRouterBuilder<TRequest> where TRequest : IRequest
{
    private readonly List<Fragment> _segmentsWithPathParameters = [];
    private readonly List<Fragment> _queryParameters = [];
    private int _segmentsWithParametersIndex;
    private int _queryParametersIndex;

    public static RouteBuilder<TRequest> Empty = new();

    public IRouterBuilder<TRequest> AddSegment(string segment)
    {
        _segmentsWithPathParameters.Add(new Fragment
        {
            Order = _segmentsWithParametersIndex++,
            Name = segment,
            IsParameter = false,
            ParameterAccessor = _ => segment
        });
        return this;
    }

    public IRouterBuilder<TRequest> AddSegments(params string[] segments)
    {
        foreach (var segment in segments) AddSegment(segment);
        return this;
    }

    public IRouterBuilder<TRequest> AddParameter(Expression<Func<TRequest, object>> expression)
    {
        _segmentsWithPathParameters.Add(new Fragment
        {
            Order = _segmentsWithParametersIndex++,
            Name = GetMemberName(expression),
            IsParameter = true,
            ParameterAccessor = expression.Compile()
        });
        return this;
    }

    public IRouterBuilder<TRequest> AddQueryParameter(Expression<Func<TRequest, object>> expression)
    {
        _queryParameters.Add(new Fragment
        {
            Order = _queryParametersIndex++,
            Name = GetMemberName(expression),
            IsParameter = true,
            ParameterAccessor = expression.Compile()
        });
        return this;
    }

    public string BuildRoutePattern()
    {
        var routePattern = string.Join("/", _segmentsWithPathParameters
            .OrderBy(f => f.Order)
            .Select(f => f.IsParameter ? "{" + f.Name + "}" : f.Name));

        return routePattern;
    }

    public Func<TRequest, string> BuildRouteGenerator()
    {
        return request =>
        {
            var path = string.Join("/", _segmentsWithPathParameters
                .OrderBy(f => f.Order)
                .Select(f => f.IsParameter ? RouteValueConverter.ConvertToString(f.ParameterAccessor(request)) : f.Name)
                .Select(Uri.EscapeDataString));

            if (_queryParameters.IsEmpty) return path;

            var query = string.Join("&", _queryParameters
                .OrderBy(f => f.Order)
                .SelectMany(f => RouteValueConverter.ConvertQuery(f.Name, f.ParameterAccessor(request)))
                .Select(f => $"{Uri.EscapeDataString(f.name)}={Uri.EscapeDataString(f.value)}"));

            path += "?" + query;

            return path;
        };
    }

    private readonly struct Fragment
    {
        public required int Order { get; init; }
        public required string Name { get; init; }
        public required bool IsParameter { get; init; }
        public required Func<TRequest, object> ParameterAccessor { get; init; }
    }

    private static string GetMemberName(Expression<Func<TRequest, object>> expression)
    {
        return expression.Body switch
        {
            MemberExpression memberExpression => memberExpression.Member.Name,
            UnaryExpression { Operand: MemberExpression operandMember } => operandMember.Member.Name,
            _ => throw new ArgumentException("Invalid expression type", nameof(expression))
        };
    }
}