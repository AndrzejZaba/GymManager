﻿using GymManager.Application.Dictionaries;
using GymManager.Application.EmployeeEvents.Commands.AddEmployeeEvent;
using GymManager.Application.EmployeeEvents.Commands.DeleteEmployeeEvent;
using GymManager.Application.EmployeeEvents.Commands.UpdateEmployeeEvent;
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

    [HttpPost]
    public async Task<IActionResult> AddEmployeeEvent(AddEmployeeEventCommand command)
    {
        await Mediator.Send(command);

        return Json(new
        {
            success = true
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateEmployeeEvent(UpdateEmployeeEventCommand command)
    {
        await Mediator.Send(command);

        return Json(new
        {
            success = true
        });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteEmployeeEvent(int id)
    {
        await Mediator.Send(new DeleteEmployeeEventCommand { Id = id });

        return Json(new
        {
            success = true
        });
    }

}
