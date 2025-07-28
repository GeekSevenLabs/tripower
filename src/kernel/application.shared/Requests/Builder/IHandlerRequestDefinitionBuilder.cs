// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IHandlerRequestDefinitionBuilder<out TRequest> where TRequest : IRequest
{
    internal IHandlerRequestDefinitionBuilder<TRequest> WithRequiredValidation(bool required = true);
    
    IHandlerRequestDefinitionBuilder<TRequest> WithRequiredAuthentication(bool required = true);
    IHandlerRequestDefinitionBuilder<TRequest> WithRequiredRoles(params string[] roles);
    IHandlerRequestDefinitionBuilder<TRequest> WithRequiredClaims(params string[] claims);
    
    IHandlerRequestDefinitionBuilder<TRequest> MapGet([StringSyntax(StringSyntaxAttribute.Uri)] string path, Func<TRequest, string> build);
    IHandlerRequestDefinitionBuilder<TRequest> MapPost([StringSyntax(StringSyntaxAttribute.Uri)] string path, Func<TRequest, string> build);
    IHandlerRequestDefinitionBuilder<TRequest> MapDelete([StringSyntax(StringSyntaxAttribute.Uri)] string path, Func<TRequest, string> build);
    
    internal IHandlerRequestDefinition Build();
}