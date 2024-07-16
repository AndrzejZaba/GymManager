using GymManager.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace GymManager.Infrastructure.SignalR.UserNotification;

public class UserNotificationService : IUserNotificationService
{
    private readonly IHubContext<NotificationUserHub> _hubContext;
    private readonly IUserConnectionManager _userConnectionManager;

    public UserNotificationService(
        IHubContext<NotificationUserHub> hubContext,
        IUserConnectionManager userConnectionManager)
    {
        _hubContext = hubContext;
        _userConnectionManager = userConnectionManager;
    }
    public async Task SendNotification(string userId, string message)
    {
        var connection = _userConnectionManager.GetUserConnections(userId);

        if (connection == null || !connection.Any())
            return;

        foreach (var connectionInfo in connection)
        {
            await _hubContext.Clients.Client(connectionInfo).SendAsync("sendToUser", message);
        }

    }
}
