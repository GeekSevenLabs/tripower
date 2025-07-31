using TriPower.Electrical.Domain.Projects;
using TriPower.Electrical.Infrastructure.Configurations;

namespace TriPower.Electrical.Infrastructure.Contexts;

public class TriElectricalDbContext(DbContextOptions<TriElectricalDbContext> options) : DbContext(options), ITriElectricalUnitOfWork
{
    public required DbSet<Project> Projects { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
    }

    public new async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await base.SaveChangesAsync(cancellationToken);
}