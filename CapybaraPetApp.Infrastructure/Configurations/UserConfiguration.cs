using CapybaraPetApp.Domain.ItemAggregate;
using CapybaraPetApp.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapybaraPetApp.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
        
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);

        builder.HasMany(user => user.Items)
            .WithMany(item => item.Users)
            .UsingEntity("User_Item");

        builder.HasMany(user => user.Capybaras)
            .WithOne()
            .HasForeignKey("UserId");
    }
}
