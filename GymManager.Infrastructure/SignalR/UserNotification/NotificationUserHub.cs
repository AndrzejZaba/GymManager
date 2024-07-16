using GymManager.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;


namespace GymManager.Infrastructure.SignalR.UserNotification;

public class NotificationUserHub : Hub
{
    private readonly IUserConnectionManager _userConnectionManager;

    public NotificationUserHub(IUserConnectionManager userConnectionManager)
    {
        _userConnectionManager = userConnectionManager;
    }

    public string GetConnectionId()
    {
        var httpContext = Context.GetHttpContext();

        var userId = httpContext.Request.Query["userId"];
        _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);

        return Context.ConnectionId;
    }

    public async override Task OnDisconnectedAsync(Exception exception)
    {
        var connectionId = Context.ConnectionId;
        _userConnectionManager.RemoveUserConnection(connectionId);
    }

}
