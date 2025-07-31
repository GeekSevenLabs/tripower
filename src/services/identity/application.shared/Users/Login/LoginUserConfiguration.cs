namespace TriPower.Identity.Application.Shared.Users.Login;

internal class LoginUserConfiguration : IHandlerRequestConfiguration<LoginUserRequest>
{
    public void OnConfigure(IHandlerRequestDefinitionBuilder<LoginUserRequest> builder)
    {
        builder
            .WithName("Login User")
            .MapPost(route =>
            {
                route.AddSegments("users", "login");
            })
            .AllowAnonymous()
            .WithValidator<LoginUserValidator>()
            .WithRequestTypeInfo(Default.LoginUserRequest);
    }
}