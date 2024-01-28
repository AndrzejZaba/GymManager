using GymManager.Application.Clients.Queries.GetClient;
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
}
