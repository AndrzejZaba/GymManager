using GymManager.Application.Roles.Commands.AddRole;
using GymManager.Application.Roles.Commands.EditRole;
using GymManager.Application.Roles.Queries.GetEditRole;
using GymManager.Application.Roles.Queries.GetRoles;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.UI.Controllers;

public class RoleController : BaseController
{
    public async Task<IActionResult> Roles()
    {
        return View(await Mediator.Send(new GetRolesQuery()));
    }

    public IActionResult AddRole()
    {
        return View(new AddRoleCommand());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddRole(AddRoleCommand command)
    {
        var result = await MediatorSendValidate(command);

        if (!result.IsValid)
            return View(command);

        TempData["Success"] = "Role zostały zaktualizowane.";

        return RedirectToAction("Roles");
    }

    public async Task<IActionResult> EditRole(string id)
    {
        return View(await Mediator.Send(new GetEditRoleQuery { Id = id}));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRole(EditRoleCommand command)
    {
        var result = await MediatorSendValidate(command);

        if (!result.IsValid)
            return View(command);

        TempData["Success"] = "Role zostały zaktualizowane.";

        return RedirectToAction("Roles");
    }

}
