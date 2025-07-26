// ReSharper disable once CheckNamespace
namespace TriPower;

public readonly record struct HandlerRequestDefinition
{
    #region Validation

    public bool RequiredValidation { get; init; }

    #endregion

    #region Authorization

    public bool RequiredAuthentication { get; init; }

    public string[] RequiredRoles { get; init; }
    public string[] RequiredClaims { get; init; }

    #endregion
    
    #region Endpoint

    public string Path { get; init; }
    public EndpointMethod Method { get; init; }
    public Func<object, string> BuildPath { get; init; }

    #endregion
}