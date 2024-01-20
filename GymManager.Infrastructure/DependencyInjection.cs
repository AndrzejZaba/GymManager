using GymManager.Application.Common.Interfaces;
using GymManager.Domain.Entities;
using GymManager.Infrastructure.Identity;
using GymManager.Infrastructure.Persistence;
using GymManager.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace GymManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options => 
        options.UseSqlServer(connectionString)
        .EnableSensitiveDataLogging());

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Password = new PasswordOptions
            {
                RequireDigit = true,
                RequiredLength = 8,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireNonAlphanumeric = true,
            };
        })
        .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
        .AddRoleManager<RoleManager<IdentityRole>>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();

        services.AddSingleton<IEmail, Email>();
        services.AddSingleton<IAppSettingsService, AppSettingsService>();


        return services;
    }
     
    public static IApplicationBuilder UseInfrastructure(
        this IApplicationBuilder app,
        IApplicationDbContext context,
        IAppSettingsService appSettingsService,
        IEmail email)        
    {
        appSettingsService.Update(context).GetAwaiter().GetResult();
        email.Update(appSettingsService).GetAwaiter().GetResult();
        return app;
    }
}
