using FluentValidation.Results;
using GymManager.Application.Common.Exceptions;
using GymManager.Application.Common.Interfaces;
using GymManager.Application.Roles.Queries.GetRoles;
using Microsoft.AspNetCore.Identity;

namespace GymManager.Infrastructure.Services;

public class RoleManagerService : IRoleManagerService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleManagerService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task CreateAsync(string roleName)
    {
        await ValidateRoleName(roleName);

        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

        if(!result.Succeeded)
            throw new Exception(string.Join(". ", result.Errors.Select(x => x.Description)));

    }

    private async Task ValidateRoleName(string roleName)
    {
        if (await _roleManager.RoleExistsAsync(roleName))
            throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("Name", $"Rola o nazwie '{roleName}' już istnieje.") });
    
    }

    public IEnumerable<RoleDto> GetRoles()
    {
        return _roleManager.Roles.Select(x => new RoleDto { Id = x.Id, Name = x.Name }).ToList();
    }
}
