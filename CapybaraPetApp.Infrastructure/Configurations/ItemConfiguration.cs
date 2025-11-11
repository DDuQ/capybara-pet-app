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
        
        //This config makes this column case-insensitive (used for validations).
        // https://learn.microsoft.com/en-us/ef/core/miscellaneous/collations-and-case-sensitivity#column-collation
        builder.Property(i => i.Name)
            .UseCollation("SQL_Latin1_General_CP1_CI_AS"); 
    }
}