namespace TriPower.Identity.Application.Shared.Users.Create;

internal class CreateUserConfiguration : IHandlerRequestConfiguration<CreateUserRequest>
{
    public void OnConfigure(IHandlerRequestDefinitionBuilder<CreateUserRequest> builder)
    {
        builder
            .WithName("Criar Usuário")
            .MapPost(route =>
            {
                route.AddSegments("users");
            })
            .AllowAnonymous()
            .WithValidator<CreateUserValidator>()
            .WithRequestTypeInfo(Default.CreateUserRequest);
    }
}