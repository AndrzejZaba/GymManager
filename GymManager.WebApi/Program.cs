using GymManager.Application;
using GymManager.Application.Common.Interfaces;
using GymManager.Infrastructure;
using GymManager.WebApi.Extensions;
using GymManager.WebApi.Middlewares;
using Microsoft.Extensions.Options;
using NLog.Web;

namespace GymManager.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(LogLevel.Information);
            builder.Logging.AddNLogWeb();

            builder.Services.AddCors();
            builder.Services.AddCulture();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddBearerAuthentication(builder.Configuration);

            builder.Services.AddControllers();

            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerBearerAuthorization();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            using (var scope = app.Services.CreateScope())
            {
                app.UseRequestLocalization(
                    app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

                app.UseInfrastructure(
                    scope.ServiceProvider.GetRequiredService<IApplicationDbContext>(),
                    app.Services.GetService<IAppSettingsService>(),
                    app.Services.GetService<IEmail>(),
                    app.Services.GetService<IWebHostEnvironment>());
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
                    options.SwaggerEndpoint($"/swagger/v2/swagger.json", "v2");
                });
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}