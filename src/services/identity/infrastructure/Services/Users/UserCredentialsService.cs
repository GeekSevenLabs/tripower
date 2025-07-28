using Microsoft.AspNetCore.Identity;
using TriPower.Identity.Application.Services.Users;
using TriPower.Identity.Domain.Users;

namespace TriPower.Identity.Infrastructure.Services.Users;

public class UserCredentialsService : IUserCredentialsService
{
    private readonly PasswordHasher<User> _hasher = new();
    
    public async Task<string> GeneratePasswordHashAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return _hasher.HashPassword(user, password);
    }

    public async Task<bool> VerifyPasswordAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);   
        return result is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
    }
}