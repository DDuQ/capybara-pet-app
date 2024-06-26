using CapybaraPetApp.Domain.AchievementAggregare;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapybaraPetApp.Infrastructure.Configurations;

public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
{
    public void Configure(EntityTypeBuilder<Achievement> builder)
    {
        builder.ToTable(nameof(Achievement));

        builder.OwnsOne(achievement => achievement.AchievementType);
    }
}