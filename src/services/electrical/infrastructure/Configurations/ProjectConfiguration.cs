using TriPower.Electrical.Domain.Projects;

namespace TriPower.Electrical.Infrastructure.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(project => project.Id);

        // Properties
        builder
            .Property(project => project.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(project => project.Description)
            .HasMaxLength(500);

        builder
            .Property(project => project.Voltage)
            .IsRequired();
        
        builder
            .Property(project => project.Phases)
            .IsRequired();

        // Relationships
        builder
            .Property(project => project.UserId)
            .IsRequired();
    }
}