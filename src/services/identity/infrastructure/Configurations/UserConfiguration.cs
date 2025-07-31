using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TriPower.Identity.Domain.Users;

namespace TriPower.Identity.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder
            .Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(256);
        
        builder
            .HasIndex(user => user.Email)
            .IsUnique();
        
        builder
            .Property(user => user.EmailConfirmed)
            .IsRequired();

        builder
            .Property(user => user.PasswordHash)
            .IsRequired()
            .HasMaxLength(1512);
        
        builder
            .Property(user => user.CreatedAt)
            .IsRequired();
        
        builder.ComplexProperty(user => user.Name, voBuilder =>
        {
            voBuilder.Property(vo => vo.First).IsRequired().HasMaxLength(256);
            voBuilder.Property(vo => vo.Last).IsRequired().HasMaxLength(256);
        });
    }
}