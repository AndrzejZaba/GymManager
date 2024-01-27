using GymManager.Application.Roles.Commands.AddRole;
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
}
