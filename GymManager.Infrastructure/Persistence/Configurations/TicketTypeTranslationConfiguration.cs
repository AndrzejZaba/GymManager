using GymManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManager.Infrastructure.Persistence.Configurations;

public class TicketTypeTranslationConfiguration : IEntityTypeConfiguration<TicketTypeTranslation>
{
    public void Configure(EntityTypeBuilder<TicketTypeTranslation> builder)
    {
        builder.ToTable("TicketTypeTranslations");

        builder.Property(x => x.Name)
            .IsRequired();

        builder.HasOne(x => x.Language)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.LanguageId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}