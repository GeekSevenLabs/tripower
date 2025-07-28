using TriPower.Identity.Domain.Users;

namespace TriPower.Identity.Application.Services.Authentications;

public interface IAuthenticationService
{
    Task<bool> AuthenticateAsync(User user, string password, CancellationToken cancellationToken = default);
    Task<bool> RefreshAuthenticateAsync(string refreshToken, CancellationToken cancellationToken = default);
}