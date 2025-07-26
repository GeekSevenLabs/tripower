using TriPower.Identity.Application.Shared.Users.Create;

namespace TriPower.Identity.Application.Shared;

public class TriIdentityRequestContext : IRequestContext
{
    public static void ConfigureRequests(IRequestRegistry registry)
    {
        registry.RegisterWithValidator<CreateUserRequest, CreateUserValidator>(builder =>
        {
            builder
                .MapPost("/users")
                .WithRequiredAuthentication(required: false);
        });
    }
}