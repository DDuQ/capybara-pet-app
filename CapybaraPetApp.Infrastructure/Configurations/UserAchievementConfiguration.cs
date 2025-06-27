using CapybaraPetApp.Domain.Common.JoinTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapybaraPetApp.Infrastructure.Configurations;

public class UserAchievementConfiguration : IEntityTypeConfiguration<UserAchievement>
{
    public void Configure(EntityTypeBuilder<UserAchievement> builder)
    {
        builder.ToTable("User_Achievement");

        builder.HasOne(userAchievement => userAchievement.User)
            .WithMany(user => user.UserAchievements)
            .HasForeignKey(userAchievement => userAchievement.UserId);

        builder.HasOne(userAchievement => userAchievement.Achievement)
            .WithMany(achievement => achievement.UserAchievements)
            .HasForeignKey(userAchievement => userAchievement.AchievementId);

        builder.HasKey(userAchievement => new { userAchievement.UserId, userAchievement.AchievementId });
    }
}
