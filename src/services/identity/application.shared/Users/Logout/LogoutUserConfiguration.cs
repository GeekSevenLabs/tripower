namespace TriPower.Identity.Application.Shared.Users.Logout;

internal class LogoutUserConfiguration : IHandlerRequestConfiguration<LogoutUserRequest>
{
    public void OnConfigure(IHandlerRequestDefinitionBuilder<LogoutUserRequest> builder)
    {
        builder
            .WithName("Logout User")
            .MapPost(route =>
            {
                route.AddSegments("users", "logout");
            })
            .RequireAuthorization()
            .WithRequestTypeInfo(Default.LogoutUserRequest);
    }
}