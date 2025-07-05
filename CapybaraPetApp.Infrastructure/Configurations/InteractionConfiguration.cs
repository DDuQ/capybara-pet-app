using CapybaraPetApp.Domain.CapybaraAggregate;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using CapybaraPetApp.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapybaraPetApp.Infrastructure.Configurations;

public class InteractionConfiguration : IEntityTypeConfiguration<Interaction>
{
    public void Configure(EntityTypeBuilder<Interaction> builder)
    {
        builder.ToTable(nameof(Interaction));

        builder.OwnsOne(i => i.InteractionDetail);

        builder.HasKey(i => new { i.Id, i.UserId, i.CapybaraId });

        builder.HasOne<User>()
            .WithMany(u => u.Interactions)
            .HasForeignKey(i => i.UserId)
            .IsRequired();

        builder.HasOne<Capybara>()
            .WithMany()
            .HasForeignKey(i => i.CapybaraId)
            .IsRequired();
    }
}