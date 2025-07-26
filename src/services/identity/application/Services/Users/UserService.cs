using TriPower.Identity.Domain.Users;

namespace TriPower.Identity.Application.Services.Users;

// TODO: Implement a proper password hashing and verification mechanism.
public class UserService : IUserService
{
    public Task<string> GeneratePasswordHashAsync(string? password, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");
        }

        // Here you would implement the actual password hashing logic.
        // For demonstration purposes, we return the password as is.
        return Task.FromResult(password);
    }

    public Task<bool> VerifyPasswordAsync(User user, string? password, CancellationToken cancellationToken = default)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

        // Here you would implement the actual password verification logic.
        // For demonstration purposes, we check if the password matches the user's password.
        var passwordHash = password;
        return Task.FromResult(user.PasswordHash == passwordHash);
    }
}