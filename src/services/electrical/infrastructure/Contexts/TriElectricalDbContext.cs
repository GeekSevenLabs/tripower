using TriPower.Electrical.Domain.Circuits;
using TriPower.Electrical.Domain.Projects;
using TriPower.Electrical.Domain.Projects.Entities;
using TriPower.Electrical.Infrastructure.Configurations;

namespace TriPower.Electrical.Infrastructure.Contexts;

public class TriElectricalDbContext(DbContextOptions<TriElectricalDbContext> options) : DbContext(options), ITriElectricalUnitOfWork
{
    public required DbSet<Project> Projects { get; init; }
    public required DbSet<Room> Rooms { get; init; }
    
    public required DbSet<Circuit> Circuits { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new CircuitConfiguration());
    }

    public new async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await base.SaveChangesAsync(cancellationToken);
}