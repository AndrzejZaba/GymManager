using GymManager.Application.Dictionaries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GymManager.UI.Controllers;

[Authorize(Roles = RolesDict.Administrator)]
public class SettingsController : BaseController
{
    public IActionResult Settings()
    {
        return View();
    }
}
