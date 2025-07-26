using TriPower.Identity.Application.Shared.Users.Create;
using TriPower.Identity.Application.Users.Create;

namespace TriPower.Identity.Application;

public class TriIdentityHandlersContext : IHandlersContext
{
    public static void ConfigureHandlers(IHandlerRegistry registry)
    {
        registry.Register<CreateUserHandler, CreateUserRequest>();
    }
}