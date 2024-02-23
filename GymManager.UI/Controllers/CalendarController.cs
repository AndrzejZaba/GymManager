using GymManager.Application.Dictionaries;
using GymManager.Application.EmployeeEvents.Queries.GetEmployeeEvents;
using GymManager.Application.Employees.Queries.GetEmployeeBasics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.UI.Controllers;

[Authorize(Roles = $"{RolesDict.Administrator},{RolesDict.Pracownik}")]
public class CalendarController : BaseController
{
    public async Task<IActionResult> Calendar()
    {
        return View(await Mediator.Send(new GetEmployeeBasicsQuery()));
    }

    public async Task<IActionResult> GetEmployeeEvents()
    {
        return Json(await Mediator.Send(new GetEmployeeEventsQuery()));
    }
}
