﻿using GymManager.Application.Roles.Queries.GetRoles;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.UI.Controllers;

public class RoleController : BaseController
{
    public async Task<IActionResult> Roles()
    {
        return View(await Mediator.Send(new GetRolesQuery()));
    }
}
