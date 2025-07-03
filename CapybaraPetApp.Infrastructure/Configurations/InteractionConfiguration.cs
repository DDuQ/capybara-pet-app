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

        builder.HasKey(interaction => new { interaction.Id, interaction.UserId, interaction.CapybaraId });
    }
}