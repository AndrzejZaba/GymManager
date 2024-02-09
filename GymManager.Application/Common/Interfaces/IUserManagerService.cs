

namespace GymManager.Application.Common.Interfaces;

public interface IUserManagerService
{
    Task<string> CreateAsync(string email, string password, string role);
}
