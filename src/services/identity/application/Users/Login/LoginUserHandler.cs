using TriPower.Identity.Application.Services.Authentications;
using TriPower.Identity.Application.Shared.Users.Login;
using TriPower.Identity.Domain.Users;

namespace TriPower.Identity.Application.Users.Login;

public class LoginUserHandler(
    IUserRepository repository,
    IAuthenticationService authentication) : IHandler<LoginUserRequest>
{
    public async Task HandleAsync(LoginUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await repository.GetByEmailAsync(request.Email!);
        Throw.When.Null(user, "Invalid email or password.");

        await authentication.AuthenticateAsync(user, request.Password!, cancellationToken);
    }
}