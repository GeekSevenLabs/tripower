namespace TriPower.Identity.Application.Services.Authentications;

public interface IAuthenticationCacheService
{
    Task SetRefreshTokenAsync(AuthenticationToken refreshToken);
    
    Task<AuthenticationToken?> GetRefreshTokenAsync(string refreshToken);
    
    Task RevokeRefreshTokenAsync(string refreshToken);
    Task<bool> IsRefreshTokenRevokedAsync(string refreshToken);
}