namespace TriPower.Identity.Application.Services.Authentications;

public class MemoryAuthenticationCacheService : IAuthenticationCacheService
{
    private static readonly Dictionary<string, AuthenticationToken> RefreshTokens = [];
    
    
    public Task SetRefreshTokenAsync(AuthenticationToken refreshToken)
    {
        RefreshTokens[refreshToken.Token] = refreshToken;
        return Task.CompletedTask;
    }

    public Task<AuthenticationToken?> GetRefreshTokenAsync(string refreshToken)
    {
        RefreshTokens.TryGetValue(refreshToken, out var token);
        return Task.FromResult(token);
    }

    public Task RevokeRefreshTokenAsync(string refreshToken)
    {
        RefreshTokens.Remove(refreshToken);
        return Task.CompletedTask;
    }

    public Task<bool> IsRefreshTokenRevokedAsync(string refreshToken)
    {
        var isRevoked = !RefreshTokens.ContainsKey(refreshToken);
        return Task.FromResult(isRevoked);
    }
}