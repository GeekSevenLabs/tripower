using System.Text;
using System.Text.Json;
using Menso.Tools.Exceptions;
using Microsoft.AspNetCore.Http;
using TriPower.Identity.Application.Services.Authentications;

namespace TriPower.Identity.Infrastructure.Services.Authentications;

public class AuthenticationCookieService(HttpContextAccessor accessor) : IAuthenticationCookieService
{
    private const string CookieAccessTokenName = "TriAccessToken";
    private const string CookieRefreshTokenName = "TriRefreshToken";

    public Task SetTokensAsync(AuthenticationTokens tokens)
    {
        Throw.When.Null(accessor.HttpContext, "HttpContext is null. Ensure that the HttpContextAccessor is properly configured in your application.");
        
        var accessTokenOptions = new CookieOptions
        {
            Expires = tokens.AccessToken.Expiration,
            HttpOnly = true,
            Secure = accessor.HttpContext.Request.IsHttps,
            SameSite = SameSiteMode.Strict
        };
        accessor.HttpContext.Response.Cookies.Append(CookieAccessTokenName, TokenToJsonBase64(tokens.AccessToken), accessTokenOptions);
        
        var refreshTokenOptions = new CookieOptions
        {
            Expires = tokens.RefreshToken.Expiration,
            HttpOnly = true,
            Secure = accessor.HttpContext.Request.IsHttps,
            SameSite = SameSiteMode.Strict
        };
        accessor.HttpContext.Response.Cookies.Append(CookieRefreshTokenName, tokens.RefreshToken.Token, refreshTokenOptions);
        return Task.CompletedTask;
    }

    public async Task<AuthenticationToken?> GetAccessTokenAsync()
    {
        Throw.When.Null(accessor.HttpContext, "HttpContext is null. Ensure that the HttpContextAccessor is properly configured in your application.");
        
        var accessTokenBase64 = await GetCookieAsync(CookieAccessTokenName);
        return string.IsNullOrEmpty(accessTokenBase64) ? null : JsonBase64ToToken(accessTokenBase64);
    }

    public async Task<string?> GetRefreshTokenAsync()
    {
        Throw.When.Null(accessor.HttpContext, "HttpContext is null. Ensure that the HttpContextAccessor is properly configured in your application.");
        var refreshToken = await GetCookieAsync(CookieRefreshTokenName);
        return string.IsNullOrEmpty(refreshToken) ? null : refreshToken;
    }

    public Task RemoveTokensAsync()
    {
        Throw.When.Null(accessor.HttpContext, "HttpContext is null. Ensure that the HttpContextAccessor is properly configured in your application.");
        accessor.HttpContext.Response.Cookies.Delete(CookieAccessTokenName);
        accessor.HttpContext.Response.Cookies.Delete(CookieRefreshTokenName);
        return Task.CompletedTask;
    }
    
    private Task<string?> GetCookieAsync(string name)
    {
        Throw.When.Null(accessor.HttpContext, "HttpContext is null. Ensure that the HttpContextAccessor is properly configured in your application.");
        accessor.HttpContext.Request.Cookies.TryGetValue(name, out var value);
        return Task.FromResult(value);
    }
    
    private static string TokenToJsonBase64(AuthenticationToken token)
    {
        var json = JsonSerializer.Serialize(token);
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
    }
    
    private static AuthenticationToken JsonBase64ToToken(string base64)
    {
        var json = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        return JsonSerializer.Deserialize<AuthenticationToken>(json) ?? throw new InvalidOperationException("Failed to deserialize AuthenticationToken from base64 string.");
    }
}