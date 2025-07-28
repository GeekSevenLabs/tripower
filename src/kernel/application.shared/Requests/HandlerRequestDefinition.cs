// ReSharper disable once CheckNamespace
namespace TriPower;

public record HandlerRequestDefinition<TRequest> : IHandlerRequestDefinition where TRequest : IRequest
{
    #region Validation

    public bool RequiredValidation { get; init; }

    #endregion

    #region Authorization

    public bool RequiredAuthentication { get; init; }

    public required string[] RequiredRoles { get; init; }
    public required string[] RequiredClaims { get; init; }

    #endregion
    
    #region Endpoint

    public required string Path { get; init; }
    public EndpointMethod Method { get; init; }
    public required Func<TRequest, string> BuildPath { get; init; }

    #endregion
}

public interface IHandlerRequestDefinition
{
    #region Validation

    public bool RequiredValidation { get; }

    #endregion

    #region Authorization

    public bool RequiredAuthentication { get; }

    public string[] RequiredRoles { get; }
    public string[] RequiredClaims { get; }

    #endregion
    
    #region Endpoint

    public string Path { get; }
    public EndpointMethod Method { get; }

    #endregion
}