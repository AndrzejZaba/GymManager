

namespace GymManager.Application.Common.Interfaces;

public interface IUserNotificationService
{
    Task SendNotification(string userId, string message);
}
