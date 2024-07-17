using GymManager.Application.Common.Interfaces;
using GymManager.Domain.Entities;
using GymManager.Infrastructure.Encryption;
using GymManager.Infrastructure.Identity;
using GymManager.Infrastructure.Invoices;
using GymManager.Infrastructure.Payments;
using GymManager.Infrastructure.Pdf;
using GymManager.Infrastructure.Persistence;
using GymManager.Infrastructure.Services;
using GymManager.Infrastructure.SignalR.UserNotification;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rotativa.AspNetCore;

namespace GymManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // rxgCe0XpNRpqI6UNFsL0XxX8AvDQPvWFEnom4WlTeZE=
        // MageyledYuyMD0RV8QEV6A==

        var encryptionService = new EncryptionService(new KeyInfo
            ("rxgCe0XpNRpqI6UNFsL0XxX8AvDQPvWFEnom4WlTeZE=", 
            "MageyledYuyMD0RV8QEV6A=="));

        services.AddSingleton<IEncryptionService>(encryptionService);

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        var connectionString = encryptionService.Decrypt(configuration.GetConnectionString("DefaultConnection"));

        services.AddDbContext<ApplicationDbContext>(options => 
        options.UseSqlServer(connectionString)
        .EnableSensitiveDataLogging());

        services.AddHostedService<LongRunningService>();
        services.AddSingleton<IBackgroundWorkerQueue, BackgroundWorkerQueue>();

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

        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<IRoleManagerService, RoleManagerService>();
        services.AddScoped<IUserRoleManagerService, UserRoleManagerService>();
        services.AddScoped<IUserManagerService, UserManagerService>();
        services.AddSingleton<IEmail, Email>();
        services.AddSingleton<IAppSettingsService, AppSettingsService>();
        services.AddHttpContextAccessor();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IHttpContext, MyHttpContext>();
        services.AddSingleton<IFileManagerService, FileManagerService>();
        services.AddHttpClient<IPrzelewy24, Przelewy24>();
        services.AddHttpClient<IGymInvoices, GymInvoices>();
        services.AddScoped<IQrCodeGenerator, QrCodeGenerator>();
        services.AddScoped<IPdfFileGenerator, RotativaPdfGenerator>();
        services.AddScoped<IRandomService, RandomService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddSignalR();
        services.AddSingleton<IUserNotificationService, UserNotificationService>();
        services.AddSingleton<IUserConnectionManager, UserConnectionManager>();

        return services;
    }
     
    public static IApplicationBuilder UseInfrastructure(
        this IApplicationBuilder app,
        IApplicationDbContext context,
        IAppSettingsService appSettingsService,
        IEmail email,
        IWebHostEnvironment webHostEnvironment)        
    {
        appSettingsService.Update(context).GetAwaiter().GetResult();
        email.Update(appSettingsService).GetAwaiter().GetResult();

        RotativaConfiguration.Setup(webHostEnvironment.WebRootPath, "Rotativa");

        return app;
    }
}
