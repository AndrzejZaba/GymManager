using GymManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManager.Infrastructure.Persistence.Configurations;

public class SettingsPositionConfiguration : IEntityTypeConfiguration<SettingsPosition>
{
    public void Configure(EntityTypeBuilder<SettingsPosition> builder)
    {
        builder.ToTable("SettingsPositions");

        builder.Property(x => x.Key)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Value)
            .HasMaxLength(1000);

        builder.Property(x => x.Description) 
            .HasMaxLength(200)
            .IsRequired();

        builder.HasOne(x => x.Settings)
            .WithMany(x => x.Positions)
            .HasForeignKey(x => x.SettingsId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

