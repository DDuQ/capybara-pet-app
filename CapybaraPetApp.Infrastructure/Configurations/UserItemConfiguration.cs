using CapybaraPetApp.Domain.Common.JoinTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapybaraPetApp.Infrastructure.Configurations;

public class UserItemConfiguration : IEntityTypeConfiguration<UserItem>
{
    public void Configure(EntityTypeBuilder<UserItem> builder)
    {
        builder.ToTable("User_Item");

        builder.HasOne(userItem => userItem.User)
            .WithMany(user => user.UserItems)
            .HasForeignKey(userItem => userItem.UserId);

        builder.HasOne(userItem => userItem.Item)
            .WithMany(item => item.UserItems)
            .HasForeignKey(userItem => userItem.ItemId);

        builder.HasKey(userItem => new { userItem.UserId, userItem.ItemId });
    }
}
