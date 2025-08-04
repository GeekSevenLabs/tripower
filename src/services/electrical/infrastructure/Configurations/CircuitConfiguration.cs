using TriPower.Electrical.Domain.Circuits;
using TriPower.Electrical.Domain.Projects;
using TriPower.Electrical.Domain.Shared;

namespace TriPower.Electrical.Infrastructure.Configurations;

internal class CircuitConfiguration : IEntityTypeConfiguration<Circuit>
{
    public void Configure(EntityTypeBuilder<Circuit> builder)
    {
        // Table
        builder.HasKey(circuit => circuit.Id);
        builder.Property(circuit => circuit.Id).ValueGeneratedNever();
        
        builder
            .HasDiscriminator(circuit => circuit.Category)
            .HasValue<LightingCircuit>(CircuitCategory.Lighting)
            .HasValue<GeneralSocketsCircuit>(CircuitCategory.GeneralSockets)
            .HasValue<SpecificCircuit>(CircuitCategory.SpecificSocket);
        
        // Properties
        builder
            .Property(circuit => circuit.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder
            .Property(circuit => circuit.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder
            .Property(circuit => circuit.Category)
            .IsRequired();
        
        // Complex Properties
        
        builder
            .ComplexProperty(circuit => circuit.Voltage, voltageBuilder =>
            {
                voltageBuilder
                    .Property(vo => vo.Type)
                    .IsRequired();
                
                voltageBuilder
                    .Property(vo => vo.Mode)
                    .IsRequired();
            });
        
        // Relationships
        
        builder
            .Property(circuit => circuit.ProjectId)
            .IsRequired();
        
        builder
            .HasOne<Project>()
            .WithMany(project => project.Circuits)
            .HasForeignKey(circuit => circuit.ProjectId)
            .HasPrincipalKey(project => project.Id);
    }
}