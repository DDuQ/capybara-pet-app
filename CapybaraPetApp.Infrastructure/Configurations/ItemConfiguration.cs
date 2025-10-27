using CapybaraPetApp.Domain.ItemAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapybaraPetApp.Infrastructure.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable(nameof(Item));

        builder.OwnsOne(i => i.ItemDetail, detail =>
        {
            detail.Property(d => d.ItemType)
                .HasConversion<string>()
                .IsRequired();

            detail.Property(d => d.BonusEffect)
                .IsRequired();
        });
    }
}