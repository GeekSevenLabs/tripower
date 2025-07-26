// ReSharper disable once CheckNamespace
namespace TriPower;

internal class HandlerRequestDefinitionBuilder : IHandlerRequestDefinitionBuilder
{
    private bool _requiredValidation = true;
    private bool _requiredAuthentication = true;
    private string[] _requiredRoles = [];
    private string[] _requiredClaims = [];
    private string? _path;
    private EndpointMethod? _method;
    
    public static HandlerRequestDefinitionBuilder Empty => new ();
    
    public IHandlerRequestDefinitionBuilder WithRequiredValidation(bool required = true)
    {
        _requiredValidation = required;
        return this;
    }
    
    public IHandlerRequestDefinitionBuilder WithRequiredAuthentication(bool required = true)
    {
        _requiredAuthentication = required;
        return this;
    }
    public IHandlerRequestDefinitionBuilder WithRequiredRoles(params string[] roles)
    {
        _requiredRoles = roles;
        return this;
    }
    public IHandlerRequestDefinitionBuilder WithRequiredClaims(params string[] claims)
    {
        _requiredClaims = claims;
        return this;
    }

    public IHandlerRequestDefinitionBuilder MapGet(string path) => WithPath(path).WithMethod(EndpointMethod.Get);
    public IHandlerRequestDefinitionBuilder MapPost(string path) => WithPath(path).WithMethod(EndpointMethod.Post);

    public IHandlerRequestDefinitionBuilder MapDelete(string path) => WithPath(path).WithMethod(EndpointMethod.Delete);

    private HandlerRequestDefinitionBuilder WithPath(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);

        _path = path;
        return this;
    }
    private HandlerRequestDefinitionBuilder WithMethod(EndpointMethod method)
    {
        if (!Enum.IsDefined(method))
        {
            throw new ArgumentOutOfRangeException(nameof(method), "Invalid endpoint method specified.");
        }

        _method = method;
        return this;    
    }

    public HandlerRequestDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(_path))
        {
            throw new InvalidOperationException("Path must be specified.");
        }

        if (_method is null)
        {
            throw new InvalidOperationException("Method must be specified.");
        }

        return new HandlerRequestDefinition
        {
            RequiredValidation = _requiredValidation,
            RequiredAuthentication = _requiredAuthentication,
            RequiredRoles = _requiredRoles,
            RequiredClaims = _requiredClaims,
            Path = _path,
            Method = _method.Value
        };
    }
}