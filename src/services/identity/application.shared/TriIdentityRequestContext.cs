using TriPower.Identity.Application.Shared.Users.Create;
using TriPower.Identity.Application.Shared.Users.Login;
using TriPower.Identity.Application.Shared.Users.Logout;

namespace TriPower.Identity.Application.Shared;

public class TriIdentityRequestContext : IRequestContext
{
    public static void ConfigureRequests(IRequestRegistry registry)
    {
        registry.RegisterWithValidator<CreateUserRequest, CreateUserValidator>(builder =>
        {
            builder
                .MapPost("/users", _ => "/users")
                .WithRequiredAuthentication(required: false);
        });

        registry.RegisterWithValidator<LoginUserRequest, LoginUserValidator>(builder =>
        {
            builder
                .MapPost("/users/login", _ => "/users/login")
                .WithRequiredAuthentication(required: false);
        });
        
        registry.Register<LogoutUserRequest>(builder =>
        {
            builder
                .MapPost("/users/logout", _ => "/users/logout")
                .WithRequiredAuthentication(required: true);
        });
        
        
    }
}