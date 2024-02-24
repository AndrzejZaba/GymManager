using GymManager.Application.Dictionaries;
using GymManager.Application.Settings.Queries.GetSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GymManager.UI.Controllers;

[Authorize(Roles = RolesDict.Administrator)]
public class SettingsController : BaseController
{
    public async Task<IActionResult> Settings()
    {
        return View(await Mediator.Send(new GetSettingsQuery()));
    }
}
