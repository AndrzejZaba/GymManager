using FluentValidation;
using GymManager.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace GymManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), 
            typeof(LoggingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), 
            typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), 
            typeof(PerformanceBehaviour<,>));

        return services;
    }
}

