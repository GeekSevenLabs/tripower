namespace TriPower.Identity.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    void Add(User user);
    
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid userId);
}