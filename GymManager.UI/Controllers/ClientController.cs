using GymManager.Application.Clients.Commands.EditClient;
using GymManager.Application.Clients.Queries.GetClient;
using GymManager.Application.Clients.Queries.GetEditClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.UI.Controllers;

[Authorize]
public class ClientController : BaseController
{
    public IActionResult Dashboard()
    {
        return View();
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
}
