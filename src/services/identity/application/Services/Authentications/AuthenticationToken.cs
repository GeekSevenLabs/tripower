namespace TriPower.Identity.Application.Services.Authentications;

public record AuthenticationToken(string Token, Guid UserId, DateTimeOffset Expiration)
{
    public bool IsExpired => DateTimeOffset.UtcNow >= Expiration;
    public bool NeedsRefresh => DateTimeOffset.UtcNow >= Expiration.AddMinutes(-10); // Refresh if within 10 minutes of expiration
}