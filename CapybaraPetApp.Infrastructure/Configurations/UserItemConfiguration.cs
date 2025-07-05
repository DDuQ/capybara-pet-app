using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.ItemAggregate;
using CapybaraPetApp.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapybaraPetApp.Infrastructure.Configurations;

public class UserItemConfiguration : IEntityTypeConfiguration<UserItem>
{
    public void Configure(EntityTypeBuilder<UserItem> builder)
    {
        builder.ToTable("User_Item");

        builder.HasKey(ui => new { ui.UserId, ui.ItemId });

        builder.HasOne<User>()
            .WithMany(u => u.UserItems)
            .HasForeignKey(ui => ui.UserId)
            .IsRequired();

        builder.HasOne<Item>()
            .WithMany()
            .HasForeignKey(ui => ui.ItemId)
            .IsRequired();
    }
}
