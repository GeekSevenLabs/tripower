using Microsoft.EntityFrameworkCore;
using TriPower.Identity.Domain.Users;
using TriPower.Identity.Infrastructure.Contexts;

namespace TriPower.Identity.Infrastructure.Repositories;

public class UserRepository(TriIdentityDbContext db) : IUserRepository
{
    public void Add(User user) => db.Users.Add(user);

    public async Task<User?> GetByEmailAsync(string email)
    {
        email = email.ToUpperInvariant();
        return await db.Users.FirstOrDefaultAsync(user => user.Email == email);
    }
}