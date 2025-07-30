using TriPower.Identity.Application.Shared.Users.Create;
using TriPower.Identity.Application.Shared.Users.Login;
using TriPower.Identity.Application.Shared.Users.Logout;

namespace TriPower.Identity.Application.Shared;

public class TriIdentityRequestContext : IRequestContext
{
    public static void ConfigureRequests(IRequestRegistry registry)
    {
        registry
            .Register(new CreateUserConfiguration())
            .Register(new LoginUserConfiguration())
            .Register(new LogoutUserConfiguration());
    }
}