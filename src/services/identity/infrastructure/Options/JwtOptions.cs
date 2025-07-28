namespace TriPower.Identity.Infrastructure.Options;

public class JwtOptions
{
    public required string SecretKey { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int AccessTokenExpirationInMinutes { get; set; }
    public required int RefreshTokenExpirationInMinutes { get; set; }
    
    public TimeSpan AccessTokenLifetime => TimeSpan.FromMinutes(AccessTokenExpirationInMinutes);
    public TimeSpan RefreshTokenLifetime => TimeSpan.FromMinutes(RefreshTokenExpirationInMinutes);
}