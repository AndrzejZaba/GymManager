using GymManager.Application.Common.Interfaces;
using GymManager.Application.Dictionaries;
using GymManager.Application.Employees.Commands.AddEmployee;
using GymManager.Application.Employees.Queries.GetEditEmployee;
using GymManager.Application.Employees.Queries.GetEmployeeBasics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.UI.Controllers;

[Authorize(Roles = RolesDict.Administrator)]
public class EmployeeController : BaseController
{
    private readonly IDateTimeService _dateTimeService;

    public EmployeeController(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
    }
    public async Task<IActionResult> Employees()
    {
        return View(await Mediator.Send(new GetEmployeeBasicsQuery()));
    }

    public async Task<IActionResult> AddEmployee()
    {
        return View(new AddEmployeeCommand { DateOfEmployment = _dateTimeService.Now });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddEmployee(AddEmployeeCommand command)
    {
        var result = await MediatorSendValidate(command);

        if (!result.IsValid)
            return View(command);

        TempData["success"] = "Pracownik został dodany.";

        return RedirectToAction("Employees");
    }
    
    public async Task<IActionResult> EditEmployee(string employeeId)
    {
        return View(await Mediator.Send(new GetEditEmployeeQuery { UserId = employeeId}));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditEmployee(EditEmployeeVm viewModel)
    {
        var result = await MediatorSendValidate(viewModel.Employee);

        if (!result.IsValid)
            return View(viewModel);

        TempData["success"] = "Dane pracownika zostały zaktualizowane.";

        return RedirectToAction("Employees");
    }


}
