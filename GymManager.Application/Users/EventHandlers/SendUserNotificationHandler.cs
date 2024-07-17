using GymManager.Application.Common.Events;
using GymManager.Application.Common.Interfaces;
using GymManager.Application.Tickets.Events;

namespace GymManager.Application.Users.EventHandlers;

public class SendUserNotificationHandler : IEventHandler<TicketPaidEvent>
{
    private readonly IUserNotificationService _userNotificationService;

    public SendUserNotificationHandler(IUserNotificationService userNotificationService)
    {
        _userNotificationService = userNotificationService;
    }
    public async Task HandleAsync(TicketPaidEvent @event)
    {
        await _userNotificationService.SendNotification(@event.UserId, "Płatność została potwierdzona. Dziękujemy za zakup karnetu");

    }
}
