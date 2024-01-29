using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.UI.Controllers
{
    [Authorize]
    public class TicketController : BaseController
    {
        public IActionResult Tickets()
        {
            return View();
        }
    }
}
