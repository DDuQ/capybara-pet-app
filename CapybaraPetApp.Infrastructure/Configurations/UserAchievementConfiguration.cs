using CapybaraPetApp.Domain.AchievementAggregate;
using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapybaraPetApp.Infrastructure.Configurations;

public class UserAchievementConfiguration : IEntityTypeConfiguration<UserAchievement>
{
    public void Configure(EntityTypeBuilder<UserAchievement> builder)
    {
        builder.ToTable("User_Achievement");

        builder.HasKey(ua => new { ua.UserId, ua.AchievementId });

        builder.HasOne<User>()
            .WithMany(u => u.UserAchievements)
            .HasForeignKey(ua => ua.UserId)
            .IsRequired();

        builder.HasOne<Achievement>()
            .WithMany()
            .HasForeignKey(ua => ua.AchievementId)
            .IsRequired();
    }
}
