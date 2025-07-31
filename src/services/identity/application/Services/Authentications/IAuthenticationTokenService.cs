using TriPower.Identity.Domain.Users;

namespace TriPower.Identity.Application.Services.Authentications;

public interface IAuthenticationTokenService
{
    Task<AuthenticationTokens> GenerateTokensAsync(User user, CancellationToken cancellationToken = default);
    Task<AuthenticationTokens> RefreshTokensAsync(User user, AuthenticationToken refreshToken, CancellationToken cancellationToken = default);
    Task<bool> ValidateRefreshTokenAsync(User user, AuthenticationToken refreshToken, CancellationToken cancellationToken = default);
}