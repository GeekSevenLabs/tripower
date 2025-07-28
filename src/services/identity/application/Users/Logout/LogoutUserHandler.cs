using TriPower.Identity.Application.Services.Authentications;
using TriPower.Identity.Application.Shared.Users.Logout;

namespace TriPower.Identity.Application.Users.Logout;

public class LogoutUserHandler(IAuthenticationService authentication) : IHandler<LogoutUserRequest>
{
    public async Task HandleAsync(LogoutUserRequest request, CancellationToken cancellationToken = default)
    {
        await authentication.LogoutAsync(cancellationToken);
    }
}