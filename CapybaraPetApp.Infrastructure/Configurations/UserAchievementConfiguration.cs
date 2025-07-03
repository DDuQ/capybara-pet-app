using CapybaraPetApp.Domain.Common.JoinTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapybaraPetApp.Infrastructure.Configurations;

public class UserAchievementConfiguration : IEntityTypeConfiguration<UserAchievement>
{
    public void Configure(EntityTypeBuilder<UserAchievement> builder)
    {
        builder.ToTable("User_Achievement");

        builder.HasKey(userAchievement => new { userAchievement.UserId, userAchievement.AchievementId });
    }
}
