using TriPower.Electrical.Domain.Projects;
using TriPower.Electrical.Domain.Projects.Entities;

namespace TriPower.Electrical.Infrastructure.Configurations;

internal class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(room => room.Id);
        builder.Property(room => room.Id).ValueGeneratedNever();
        
        // Properties
        builder
            .Property(room => room.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder
            .Property(room => room.Perimeter)
            .IsRequired()
            .HasPrecision(18, 2);
        
        builder
            .Property(room => room.Area)
            .IsRequired()
            .HasPrecision(18, 2);
        
        builder
            .Property(room => room.Classification)
            .IsRequired();
        
        builder
            .Property(room => room.Type)
            .IsRequired();
        
        builder
            .Property(room => room.CreatedAt)
            .IsRequired();
        
        // Complex Properties
        
        builder
            .OwnsOne(room => room.Lighting, lightingBuilder =>
            {
                lightingBuilder
                    .Property(lighting => lighting.MinimumLoad)
                    .IsRequired();
            });
        
        builder
            .OwnsOne(room => room.GeneralSockets, socketsBuilder =>
            {
                socketsBuilder.Property(sockets => sockets.RequiredLoad).IsRequired();
                socketsBuilder.Property(sockets => sockets.RequiredCount).IsRequired();
                socketsBuilder.Property(sockets => sockets.Modifier).IsRequired();
                socketsBuilder.Property(sockets => sockets.CorrectedLoad).IsRequired();
                socketsBuilder.Property(sockets => sockets.CorrectedCount).IsRequired();
            });
        
        // Relationships
        
        builder
            .HasOne<Project>()
            .WithMany(project => project.Rooms)
            .HasForeignKey(room => room.ProjectId)
            .HasPrincipalKey(project => project.Id);
    }
}