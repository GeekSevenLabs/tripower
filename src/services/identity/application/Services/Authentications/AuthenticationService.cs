using TriPower.Identity.Application.Services.Users;
using TriPower.Identity.Domain.Users;

namespace TriPower.Identity.Application.Services.Authentications;

public class AuthenticationService(
    IUserCredentialsService userCredentials,
    IAuthenticationTokenService authenticationToken,
    IAuthenticationCookieService authenticationCookie,
    IAuthenticationCacheService authenticationCache,
    IUserRepository repository) : IAuthenticationService
{
    private const string RequiredNewAuthenticationMessage = "Required new authentication.";
    
    public async Task<bool> AuthenticateAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        Throw<UnauthorizedAccessException>.When.False(
            await userCredentials.VerifyPasswordAsync(user, password, cancellationToken), 
            "Email or password is incorrect."
        );

        var tokens = await authenticationToken.GenerateTokensAsync(user, cancellationToken);

        await authenticationCookie.RemoveTokensAsync();
        await authenticationCookie.SetTokensAsync(tokens);

        await authenticationCache.SetRefreshTokenAsync(tokens.RefreshToken);
        
        return true;
    }

    public async Task<AuthenticationTokens> RefreshAuthenticateAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        Throw<UnauthorizedAccessException>.When.NullOrEmpty(refreshToken, RequiredNewAuthenticationMessage);
        Throw<UnauthorizedAccessException>.When.False(
            await authenticationCache.IsRefreshTokenRevokedAsync(refreshToken), 
            RequiredNewAuthenticationMessage
        );

        var refreshTokenCache = await authenticationCache.GetRefreshTokenAsync(refreshToken);
        Throw<UnauthorizedAccessException>.When.Null(refreshTokenCache, RequiredNewAuthenticationMessage);
        Throw<UnauthorizedAccessException>.When.False(refreshTokenCache.IsExpired, RequiredNewAuthenticationMessage);

        await authenticationCache.RevokeRefreshTokenAsync(refreshToken);
        
        var user = await repository.GetByIdAsync(refreshTokenCache.UserId);
        Throw<UnauthorizedAccessException>.When.Null(user, RequiredNewAuthenticationMessage);

        var tokens = await authenticationToken.RefreshTokensAsync(user, refreshTokenCache, cancellationToken);
        
        await authenticationCookie.RemoveTokensAsync();
        await authenticationCookie.SetTokensAsync(tokens);

        await authenticationCache.SetRefreshTokenAsync(tokens.RefreshToken);
        
        return tokens;
    }
}