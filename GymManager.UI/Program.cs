using GymManager.Application;
using GymManager.Infrastructure;
using GymManager.UI.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using NLog.Web;

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

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            builder.Services.DefineViewLocation(builder.Configuration);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddSingleton<IEmail, Email>(); // Wsz�dzie gdzie w kodzie u�yjemy interfejsu IEmail zostanie on zast�piony implementacj� klasy Email - jako, �e jest to Singleton to w ca�ym programie b�dzie tylko jeden obiekt klasy Email
            //builder.Services.AddScoped<IEmail, Email>(); // Wsz�dzie gdzie w kodzie u�yjemy interfejsu IEmail zostanie on zast�piony implementacj� klasy Email - jako, �e jest to Scoped b�dzie jedna instacja dla ce�ego requestu
            //builder.Services.AddTransient<IEmail, Email>(); // Wsz�dzie gdzie w kodzie u�yjemy interfejsu IEmail zostanie on zast�piony implementacj� klasy Email - jako, �e jest to Transient, b�dzie nowa instancja dla ka�dego kontrolera lub serwisu

            var app = builder.Build();

            app.UseInfrastructure();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
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