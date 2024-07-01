using GymManager.Application;
using GymManager.Application.Common.Interfaces;
using GymManager.Infrastructure;

namespace GymManager.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                app.UseInfrastructure(
                    scope.ServiceProvider.GetRequiredService<IApplicationDbContext>(),
                    app.Services.GetService<IAppSettingsService>(),
                    app.Services.GetService<IEmail>(),
                    app.Services.GetService<IWebHostEnvironment>());
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}