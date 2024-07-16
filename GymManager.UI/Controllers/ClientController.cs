﻿using GymManager.Application.Clients.Commands.AddClient;
using GymManager.Application.Clients.Commands.EditClient;
using GymManager.Application.Clients.Queries.GetClient;
using GymManager.Application.Clients.Queries.GetClientDashboard;
using GymManager.Application.Clients.Queries.GetClientsBasics;
using GymManager.Application.Clients.Queries.GetEditAdminClient;
using GymManager.Application.Clients.Queries.GetEditClient;
using GymManager.Application.Common.Interfaces;
using GymManager.Application.Dictionaries;
using GymManager.Infrastructure.SignalR.UserNotification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.UI.Controllers;

[Authorize]
public class ClientController : BaseController
{
    private readonly IUserNotificationService _userNotificationService;

    public ClientController(IUserNotificationService userNotificationService)
    {
        _userNotificationService = userNotificationService;
    }
    public async Task<IActionResult> Dashboard()
    {
        await _userNotificationService.SendNotification(UserId, "Płatność została potwierdzona. Dziękujemy za zakup karnetu");
        return View(await Mediator.Send(new GetClientDashboardQuery
        {
            UserId = UserId
        }));
    }
    public async Task<IActionResult> Client()
    {
        return View(await Mediator.Send(new GetClientQuery { UserId = UserId }));
    }
    
    public async Task<IActionResult> EditClient()
    {
        return View(await Mediator.Send(new GetEditClientQuery { UserId = UserId }));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditClient(EditClientCommand command)
    {
        var result = await MediatorSendValidate(command);

        if (!result.IsValid)
            return View(command);

        TempData["Success"] = "Twoje dane zostały zaktualizowane.";

        return RedirectToAction("Client");   
    }

    [Authorize(Roles = $"{RolesDict.Administrator},{RolesDict.Pracownik}")]
    public async Task<IActionResult> Clients()
    {
        return View(await Mediator.Send(new GetClientsBasicsQuery()));
    }

    [Authorize(Roles = $"{RolesDict.Administrator},{RolesDict.Pracownik}")]
    public async Task<IActionResult> AddClient()
    {
        return View(new AddClientCommand());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = $"{RolesDict.Administrator},{RolesDict.Pracownik}")]
    public async Task<IActionResult> AddClient(AddClientCommand command)
    {
        var result = await MediatorSendValidate(command);

        if (!result.IsValid)
            return View(command);

        TempData["Success"] = "Dane o klientach zostały zaktualizowane.";

        return RedirectToAction("Clients");
    }


    [Authorize(Roles = $"{RolesDict.Administrator},{RolesDict.Pracownik}")]
    public async Task<IActionResult> EditAdminClient(string clientId)
    {
        return View(await Mediator.Send(new GetEditAdminClientQuery { UserId = clientId }));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = $"{RolesDict.Administrator},{RolesDict.Pracownik}")]
    public async Task<IActionResult> EditAdminClient(EditAdminClientVm vm)
    {
        var result = await MediatorSendValidate(vm.Client);

        if (!result.IsValid)
            return View(vm);

        TempData["Success"] = "Dane o klientach zostały zaktualizowane.";

        return RedirectToAction("Clients");
    }
}
