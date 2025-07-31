using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Menso.Tools.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TriPower.Identity.Application.Services.Authentications;
using TriPower.Identity.Domain.Users;
using TriPower.Identity.Infrastructure.Options;

namespace TriPower.Identity.Infrastructure.Services.Authentications;

public class AuthenticationTokenService(IOptions<JwtOptions> options) : IAuthenticationTokenService
{
    private readonly JwtOptions _options = options.Value;
    
    public Task<AuthenticationTokens> GenerateTokensAsync(User user, CancellationToken cancellationToken = default)
    {
        Throw<UnauthorizedAccessException>.When.Null(user, "User cannot be null.");
        
        var accessToken = GenerateJwt(user);
        var refreshToken = GenerateRefreshToken(user);
        return Task.FromResult( new AuthenticationTokens(accessToken, refreshToken));
    }

    public async Task<AuthenticationTokens> RefreshTokensAsync(User user, AuthenticationToken refreshToken, CancellationToken cancellationToken = default)
    {
        Throw<UnauthorizedAccessException>.When.NullOrEmpty(refreshToken.Token, "Refresh token cannot be null.");
        Throw<UnauthorizedAccessException>.When.True(refreshToken.IsExpired, "Refresh token is expired.");
        Throw<UnauthorizedAccessException>.When.NotEqual(refreshToken.UserId, user.Id, "Refresh token is invalid.");
        
        return await GenerateTokensAsync(user, cancellationToken);
    }

    public async Task<bool> ValidateRefreshTokenAsync(User user, AuthenticationToken refreshToken, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;

        return user switch
        {
            null => false,
            _ when refreshToken.IsExpired => false,
            _ when refreshToken.UserId != user.Id => false,
            _ => true
        };
    }
    
    private AuthenticationToken GenerateJwt(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Name.FullName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.Name.Last),
            new Claim(ClaimTypes.Name, user.Name.First),
            new Claim(ClaimTypes.NameIdentifier, user.Name.First),
            new Claim(TriClaimTypes.UserId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var expires = DateTimeOffset.UtcNow.Add(_options.AccessTokenLifetime);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expires.UtcDateTime,
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new AuthenticationToken(tokenString, user.Id, expires);
    }
    
    private AuthenticationToken GenerateRefreshToken(User user)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var expires = DateTimeOffset.UtcNow.Add(_options.RefreshTokenLifetime);
        return new AuthenticationToken(token, user.Id, expires);
    }
}