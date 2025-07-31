using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization.Metadata;
using FluentValidation;

// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IHandlerRequestDefinition
{
    string Name { get; }
    
    internal void ChangeName(string name);
}

public interface ISecurityDefinition
{
    bool RequiredAuthentication { get; }
    string[] RequiredRoles { get; }
    string[] RequiredClaims { get; }
    
    internal void ChangeAuthentication(bool requiredAuthentication, string[] requiredRoles, string[] requiredClaims);
}

public interface IValidationDefinition<out TRequest> where TRequest : IRequest
{
    [MemberNotNullWhen(true, nameof(ValidatorType))]
    bool RequiredValidation { get; }
    public Type? ValidatorType { get; }
    
    internal void ChangeValidator<TValidator>() where TValidator : IValidator<TRequest>;
}

public interface ISerializationDefinition<TRequest> 
    where TRequest : IRequest
{
    JsonTypeInfo<TRequest> RequestTypeInfo { get; }
    internal void ChangeRequestTypeInfo(JsonTypeInfo<TRequest> requestTypeInfo);
}

public interface ISerializationDefinition<TRequest, TResponse> :
    ISerializationDefinition<TRequest>
    where TRequest : IRequest, IRequest<TResponse> 
    where TResponse : class
{
    JsonTypeInfo<TResponse> ResponseTypeInfo { get; }
    internal void ChangeResponseTypeInfo(JsonTypeInfo<TResponse> responseTypeInfo);
}

public interface IEndpointDefinition<TRequest> where TRequest : IRequest
{
    string Path { get; }
    EndpointMethod Method { get; }
    Func<TRequest, string> BuildPath { get; }
    
    internal void ChangeEndpoint(string path, EndpointMethod method, Func<TRequest, string> buildPath);
}

public class HandlerRequestDefinition<TRequest> : 
    IHandlerRequestDefinition,
    ISecurityDefinition,
    IValidationDefinition<TRequest>,
    IEndpointDefinition<TRequest>,
    ISerializationDefinition<TRequest>
    where TRequest : IRequest
{
    public string Name { get; private set; } = string.Empty;
    public void ChangeName(string name) => Name = name;

    public bool RequiredAuthentication { get; private set; }
    public string[] RequiredRoles { get; private set; } = [];
    public string[] RequiredClaims { get; private set; } = [];
    public void ChangeAuthentication(bool requiredAuthentication, string[] requiredRoles, string[] requiredClaims)
    {
        RequiredAuthentication = requiredAuthentication;
        RequiredRoles = requiredRoles;
        RequiredClaims = requiredClaims;
    }

    [MemberNotNullWhen(true, nameof(ValidatorType))]
    public bool RequiredValidation { get; private set; }
    public Type? ValidatorType { get; private set; }
    public void ChangeValidator<TValidator>() where TValidator : IValidator<TRequest>
    {
        RequiredValidation = true;
        ValidatorType = typeof(TValidator);
    }

    public string Path { get; private set; } = string.Empty;
    public EndpointMethod Method { get; private set; }
    public Func<TRequest, string> BuildPath { get; private set; } = _ => string.Empty;
    public void ChangeEndpoint(string path, EndpointMethod method, Func<TRequest, string> buildPath)
    {
        Path = path;
        Method = method;
        BuildPath = buildPath;
    }

    public JsonTypeInfo<TRequest> RequestTypeInfo { get; private set; } = null!;
    public void ChangeRequestTypeInfo(JsonTypeInfo<TRequest> requestTypeInfo) => RequestTypeInfo = requestTypeInfo;
}

public class HandlerRequestDefinition<TRequest, TResponse> : HandlerRequestDefinition<TRequest>,
    IHandlerRequestDefinition,
    ISecurityDefinition,
    IValidationDefinition<TRequest>,
    IEndpointDefinition<TRequest>,
    ISerializationDefinition<TRequest>,
    ISerializationDefinition<TRequest, TResponse>
    where TRequest : IRequest, IRequest<TResponse> where TResponse : class
{
    public JsonTypeInfo<TResponse> ResponseTypeInfo { get; private set; } = null!;
    public void ChangeResponseTypeInfo(JsonTypeInfo<TResponse> responseTypeInfo) => ResponseTypeInfo = responseTypeInfo;
}