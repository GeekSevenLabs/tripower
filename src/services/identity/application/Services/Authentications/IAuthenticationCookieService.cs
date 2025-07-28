namespace TriPower.Identity.Application.Services.Authentications;

public interface IAuthenticationCookieService
{
    Task SetTokensAsync(AuthenticationTokens tokens);
    
    Task<AuthenticationToken?> GetAccessTokenAsync();
    Task<string?> GetRefreshTokenAsync();
    
    Task RemoveTokensAsync();
}