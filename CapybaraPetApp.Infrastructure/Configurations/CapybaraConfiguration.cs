using CapybaraPetApp.Domain.CapybaraAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapybaraPetApp.Infrastructure.Configurations;

public class CapybaraConfiguration : IEntityTypeConfiguration<Capybara>
{
    public void Configure(EntityTypeBuilder<Capybara> builder)
    {
        builder.ToTable(nameof(Capybara));
    }
}
