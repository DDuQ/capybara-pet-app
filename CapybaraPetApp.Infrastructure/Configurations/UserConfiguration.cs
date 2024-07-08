using CapybaraPetApp.Domain.CapybaraAggregate;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using CapybaraPetApp.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapybaraPetApp.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.HasMany(user => user.Items)
            .WithMany(item => item.Users)
            .UsingEntity("User_Item");

        builder.HasMany(user => user.Capybara);
    }
}
