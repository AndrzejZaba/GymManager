using GymManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManager.Infrastructure.Persistence.Configurations;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("Lnguages");

        builder.Property(x => x.Name)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Key)
            .HasMaxLength(5)
            .IsRequired();

    }
}

