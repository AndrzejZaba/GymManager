

namespace GymManager.Application.Common.Events;

public interface IEventHandler<T> where T : class, IEvent
{
    Task HandleAsync(T @event);
}
