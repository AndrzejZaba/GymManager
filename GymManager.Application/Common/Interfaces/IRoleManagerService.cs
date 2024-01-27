using GymManager.Application.Roles.Queries.GetRoles;

namespace GymManager.Application.Common.Interfaces;

public interface IRoleManagerService
{
    IEnumerable<RoleDto> GetRoles();
    Task CreateAsync(string roleName);
    Task UpdateAsync(RoleDto role);
    Task<RoleDto> FindByIdAsync(string id);
}
