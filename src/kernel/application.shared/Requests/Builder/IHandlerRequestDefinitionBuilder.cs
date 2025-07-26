// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IHandlerRequestDefinitionBuilder
{
    internal IHandlerRequestDefinitionBuilder WithRequiredValidation(bool required = true);
    
    IHandlerRequestDefinitionBuilder WithRequiredAuthentication(bool required = true);
    IHandlerRequestDefinitionBuilder WithRequiredRoles(params string[] roles);
    IHandlerRequestDefinitionBuilder WithRequiredClaims(params string[] claims);
    
    IHandlerRequestDefinitionBuilder MapGet([StringSyntax(StringSyntaxAttribute.Uri)] string path);
    IHandlerRequestDefinitionBuilder MapPost([StringSyntax(StringSyntaxAttribute.Uri)] string path);
    IHandlerRequestDefinitionBuilder MapDelete([StringSyntax(StringSyntaxAttribute.Uri)] string path);
    
    internal HandlerRequestDefinition Build();
}