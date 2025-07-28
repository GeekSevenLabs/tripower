using TriPower.Identity.Domain.Users;

namespace TriPower.Identity.Application.Services.Users;

public interface IUserCredentialsService
{
    Task<string> GeneratePasswordHashAsync(User user, string password, CancellationToken cancellationToken = default);
    Task<bool> VerifyPasswordAsync(User user, string password, CancellationToken cancellationToken = default);
}