using GymManager.Application.Common.Events;
using Microsoft.Extensions.DependencyInjection;

namespace GymManager.Infrastructure.Events;

public class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<T>(T @event) where T : class, IEvent
    {
        using var scope = _serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<T>>();

        var tasks = handlers.Select(x => x.HandleAsync(@event));

        await Task.WhenAll(tasks); // jeżeli chcemy czekać na wykonanie wszystkich subskrybujących Handlerów
        // Jeśli nie, to można wykorzystać fire and forget
    }
}
