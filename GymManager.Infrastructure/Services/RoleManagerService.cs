using GymManager.Application.Common.Interfaces;
using GymManager.Application.Roles.Queries.GetRoles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Infrastructure.Services;

public class RoleManagerService : IRoleManagerService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleManagerService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IEnumerable<RoleDto> GetRoles()
    {
        return _roleManager.Roles.Select(x => new RoleDto { Id = x.Id, Name = x.Name }).ToList();
    }
}
