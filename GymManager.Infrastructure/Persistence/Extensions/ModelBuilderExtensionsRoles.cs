using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Infrastructure.Persistence.Extensions;

static class ModelBuilderExtensionsRoles
{
    public static void SeedRoels(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "80B6EE4B-8640-4D67-8636-C7BFA8F2451D",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = "B89055F6-4241-4A90-8163-7B845C099FAA"
            },
            new IdentityRole
            {
                Id = "20E5D261-073E-4395-9E83-BEEC2DCD2EC4",
                Name = "Klient",
                NormalizedName = "KLIENT",
                ConcurrencyStamp = "F2CE9090-4855-4E8E-BCCB-0B75C5B3BFAB"
            },
            new IdentityRole
            {
                Id = "6E9BEA3A-DD4A-4D24-BA70-ABCE4D1CB47F",
                Name = "Pracownik",
                NormalizedName = "PRACOWNIK",
                ConcurrencyStamp = "3EB92419-3A20-48CF-BF34-8457B8B9A96D"
            }
            );
            
    }
}
