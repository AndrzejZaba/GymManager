using GymManager.Application.Dictionaries;
using GymManager.Application.EmployeeEvents.Queries.GetEmployeeEvents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.UI.Controllers;

[Authorize(Roles = $"{RolesDict.Administrator},{RolesDict.Pracownik}")]
public class CalendarController : BaseController
{
    public IActionResult Calendar()
    {
        return View();
    }

    public async Task<IActionResult> GetEmployeeEvents()
    {
        return Json(await Mediator.Send(new GetEmployeeEventsQuery()));
    }
}
