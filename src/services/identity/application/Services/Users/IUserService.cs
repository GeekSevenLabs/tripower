using TriPower.Identity.Domain.Users;

namespace TriPower.Identity.Application.Services.Users;

public interface IUserService
{
    Task<string> GeneratePasswordHashAsync(string? password, CancellationToken cancellationToken = default);
    Task<bool> VerifyPasswordAsync(User user, string? password, CancellationToken cancellationToken = default);
}