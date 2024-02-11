using GymManager.Domain.Entities;
using Microsoft.AspNetCore.Identity;


namespace GymManager.Application.Common.Interfaces;

public interface IUserRoleManagerService
{
    Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string roleName);
    Task<IEnumerable<IdentityRole>> GetRolesAsync(string userId);
    Task AddToRoleAsync(string userId, string roleName);
    Task RomoveFromRoleAsync(string userId, string roleName);
}
