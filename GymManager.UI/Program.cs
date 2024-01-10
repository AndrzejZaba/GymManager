using GymManager.Application;
using GymManager.Application.Common.Interfaces;
using GymManager.Infrastructure;
using GymManager.UI.Extensions;
using GymManager.UI.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using NLog.Web;
using System.Globalization;

namespace GymManager.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(LogLevel.Information);
            builder.Logging.AddNLogWeb();

            builder.Services.AddCulture();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.DefineViewLocation(builder.Configuration);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddSingleton<IEmail, Email>(); // Wszêdzie gdzie w kodzie u¿yjemy interfejsu IEmail zostanie on zast¹piony implementacj¹ klasy Email - jako, ¿e jest to Singleton to w ca³ym programie bêdzie tylko jeden obiekt klasy Email
            //builder.Services.AddScoped<IEmail, Email>(); // Wszêdzie gdzie w kodzie u¿yjemy interfejsu IEmail zostanie on zast¹piony implementacj¹ klasy Email - jako, ¿e jest to Scoped bêdzie jedna instacja dla ce³ego requestu
            //builder.Services.AddTransient<IEmail, Email>(); // Wszêdzie gdzie w kodzie u¿yjemy interfejsu IEmail zostanie on zast¹piony implementacj¹ klasy Email - jako, ¿e jest to Transient, bêdzie nowa instancja dla ka¿dego kontrolera lub serwisu

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                app.UseInfrastructure(
                    scope.ServiceProvider.GetRequiredService<IApplicationDbContext>(),
                    app.Services.GetService<IAppSettingsService>(),
                    app.Services.GetService<IEmail>());
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            var logger = app.Services.GetService<ILogger<Program>>();

            if(app.Environment.IsDevelopment())
            {
                logger.LogInformation("DEVELOPMENT MODE!");
            }
            else
            {
                logger.LogInformation("PRODUCTION MODE!");

            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}