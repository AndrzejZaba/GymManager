using GymManager.Application.Dictionaries;
using GymManager.Domain.Entities;
using GymManager.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Infrastructure.Persistence.Extensions;

static class ModelBuilderExtensionsAnnouncement
{
    public static void SeedAnnouncement(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>().HasData(
            new Announcement
            {
                Id = 1,
                Date = new DateTime(2024, 1, 6),
                Description = "Kod promocyjny na suplementy w sklepie ABC = rabat12."
            },
            new Announcement
            {
                Id = 2,
                Date = new DateTime(2024,1,5),
                Description = "W najbliższą niedzielę siłownia jest otwarta do godziny 24:00."
            },
            new Announcement
            {
                Id = 3,
                Date = new DateTime(2024, 1,5),
                Description = "Jutrzejsze zajęcia Crossfit zostały odwołane. Przepraszamy."
            },
            new Announcement
            {
                Id = 4,
                Date = new DateTime(2024, 1, 4),
                Description = "Zatrudnimy trenera personalnego."
            },
            new Announcement
            {
                Id =5,
                Date = new DateTime(2024, 1, 4),
                Description = "Od przyszłego miesiąca możesz kupić karnet dla swóch osób w cenie jednego."
            },
            new Announcement
            {
                Id = 6,
                Date = new DateTime(2024, 1, 3),
                Description = "Zatrudnimy recepcjonistę."
            },
            new Announcement
            {
                Id = 7,
                Date = new DateTime(2024, 1, 2),
                Description = "W poprzednim miesiącu zrobiłeś aż 12 treningów - gratulacje!"
            },
            new Announcement
            {
                Id = 8,
                Date = new DateTime(2024, 1, 1),
                Description = "Szczęśliwego Nowego Roku!"
            },
            new Announcement
            {
                Id = 9,
                Date = new DateTime(2024, 1, 1),
                Description = "Odbierz kod zniżkowy na suplementację w recepcji"
            },
            new Announcement
            {
                Id = 10,
                Date = new DateTime(2024, 1, 1),
                Description = "Kod promocyjny na suplementy w sklepie ABC = rabat12."
            },
            new Announcement
            {
                Id = 11,
                Date = new DateTime(2024, 1, 1),
                Description = "Kod promocyjny na suplementy w sklepie ABC = rabat12."
            },
            new Announcement
            {
                Id = 12,
                Date = new DateTime(2024,1,1),
                Description = "Kod promocyjny na suplementy w sklepie ABC = rabat12."
            }
            );
    }
}
