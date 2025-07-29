using Microsoft.EntityFrameworkCore;
using TriPower.Identity.Domain;
using TriPower.Identity.Domain.Users;
using TriPower.Identity.Infrastructure.Configurations;

namespace TriPower.Identity.Infrastructure.Contexts;

public class TriIdentityDbContext(DbContextOptions<TriIdentityDbContext> options) : DbContext(options), ITriIdentityUnitOfWork
{
    public required DbSet<User> Users { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entity>().Property(entity => entity.CreatedAt).IsRequired();
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

    public new Task SaveChangesAsync(CancellationToken cancellationToken = default) => base.SaveChangesAsync(cancellationToken);
}