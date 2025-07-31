using TriPower.Identity.Application.Shared.Users.Create;
using TriPower.Identity.Application.Shared.Users.Login;
using TriPower.Identity.Application.Shared.Users.Logout;
using TriPower.Identity.Application.Users.Create;
using TriPower.Identity.Application.Users.Login;
using TriPower.Identity.Application.Users.Logout;

namespace TriPower.Identity.Application;

public class TriIdentityHandlersContext : IHandlersContext
{
    public static void ConfigureHandlers(IHandlerRegistry registry)
    {
        registry.Register<CreateUserHandler, CreateUserRequest>();
        registry.Register<LoginUserHandler, LoginUserRequest>();
        registry.Register<LogoutUserHandler, LogoutUserRequest>();
    }
}