using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapybaraPetApp.Infrastructure.Configurations;

public class InteractionConfiguration : IEntityTypeConfiguration<Interaction>
{
    public void Configure(EntityTypeBuilder<Interaction> builder)
    {
        builder.ToTable(nameof(Interaction));

        builder.OwnsOne(interaction => interaction.InteractionDetail);

        builder.HasOne(interaction => interaction.User)
            .WithMany(user => user.Interactions)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(interaction => interaction.Capybara)
            .WithMany(capybara => capybara.Interactions)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasKey(interaction => new { interaction.UserId, interaction.CapybaraId });
    }
}