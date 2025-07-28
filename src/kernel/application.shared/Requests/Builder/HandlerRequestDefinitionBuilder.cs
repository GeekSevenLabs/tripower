// ReSharper disable once CheckNamespace
namespace TriPower;

internal class HandlerRequestDefinitionBuilder<TRequest> : IHandlerRequestDefinitionBuilder<TRequest> where TRequest : IRequest
{
    private bool _requiredValidation = true;
    private bool _requiredAuthentication = true;
    private string[] _requiredRoles = [];
    private string[] _requiredClaims = [];
    private string? _path;
    private  Func<TRequest, string>? _pathBuild;
    private EndpointMethod? _method;
    
    public static HandlerRequestDefinitionBuilder<TRequest> Empty => new ();
    
    public IHandlerRequestDefinitionBuilder<TRequest> WithRequiredValidation(bool required = true)
    {
        _requiredValidation = required;
        return this;
    }
    
    public IHandlerRequestDefinitionBuilder<TRequest> WithRequiredAuthentication(bool required = true)
    {
        _requiredAuthentication = required;
        return this;
    }
    public IHandlerRequestDefinitionBuilder<TRequest> WithRequiredRoles(params string[] roles)
    {
        _requiredRoles = roles;
        return this;
    }
    public IHandlerRequestDefinitionBuilder<TRequest> WithRequiredClaims(params string[] claims)
    {
        _requiredClaims = claims;
        return this;
    }

    public IHandlerRequestDefinitionBuilder<TRequest> MapGet(string path, Func<TRequest, string> build) => WithPath(path).WithPathBuild(build).WithMethod(EndpointMethod.Get);
    public IHandlerRequestDefinitionBuilder<TRequest> MapPost(string path, Func<TRequest, string> build) => WithPath(path).WithPathBuild(build).WithMethod(EndpointMethod.Post);
    public IHandlerRequestDefinitionBuilder<TRequest> MapDelete(string path, Func<TRequest, string> build) => WithPath(path).WithPathBuild(build).WithMethod(EndpointMethod.Delete);

    private HandlerRequestDefinitionBuilder<TRequest> WithPath(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);

        _path = path;
        return this;
    }
    
    private HandlerRequestDefinitionBuilder<TRequest> WithPathBuild(Func<TRequest, string> build)
    {
        _pathBuild = build;
        return this;
    }
    
    private HandlerRequestDefinitionBuilder<TRequest> WithMethod(EndpointMethod method)
    {
        if (!Enum.IsDefined(method))
        {
            throw new ArgumentOutOfRangeException(nameof(method), "Invalid endpoint method specified.");
        }

        _method = method;
        return this;    
    }

    public IHandlerRequestDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(_path))
        {
            throw new InvalidOperationException("Path must be specified.");
        }

        if (_method is null)
        {
            throw new InvalidOperationException("Method must be specified.");
        }
        
        if (_pathBuild is null)
        {
            throw new InvalidOperationException("Path build function must be specified.");
        }

        return new HandlerRequestDefinition<TRequest>
        {
            RequiredValidation = _requiredValidation,
            RequiredAuthentication = _requiredAuthentication,
            RequiredRoles = _requiredRoles,
            RequiredClaims = _requiredClaims,
            Path = _path,
            BuildPath = _pathBuild,
            Method = _method.Value
        };
    }
}