using CapybaraPetApp.Domain.CapybaraAggregate;
using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapybaraPetApp.Infrastructure.Configurations;

public class UserCapybaraConfiguration : IEntityTypeConfiguration<UserCapybara>
{
    public void Configure(EntityTypeBuilder<UserCapybara> builder)
    {
        builder.ToTable("User_Capybara");
        builder.HasKey(uc => new { uc.UserId, uc.CapybaraId });

        builder.HasOne(uc => uc.User)
            .WithMany(u => u.UserCapybaras)       
            .HasForeignKey(uc => uc.UserId)
            .IsRequired();

        builder.HasOne(uc => uc.Capybara)
            .WithMany()           
            .HasForeignKey(uc => uc.CapybaraId)
            .IsRequired();
    }
}
