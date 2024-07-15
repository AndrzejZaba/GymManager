using AspNetCore.ReCaptcha;
using GymManager.Application.Common.Exceptions;
using GymManager.Application.Common.Interfaces;
using GymManager.Application.Contacts.Commands.SendContactEmail;
using GymManager.Application.Tickets.Commands.AddTicket;
using GymManager.Application.Tickets.Queries.GetTicketById;
using GymManager.UI.Models;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymManager.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IDateTimeService _dateTimeService;

        public HomeController(ILogger<HomeController> logger, IDateTimeService dateTimeService)
        {
            _logger = logger;
            _dateTimeService = dateTimeService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View(new SendContactEmailCommand());
        }

        [ValidateReCaptcha]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Contact(SendContactEmailCommand command)
        {
            var result = await MediatorSendValidate(command);

            if (!result.IsValid)
            {
                ModelState.AddModelError("AntySpamResult", @"Wypełnij pole 
                    ReCaptcha (zabezpieczenie przed spamem)");
                return View(command);
            }

            TempData["Success"] = "Wiadomość została wysłana do administratora.";

            return RedirectToAction("Contact");
        }

        [HttpPost]
        public IActionResult SetCulture(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = _dateTimeService.Now.AddYears(1)}
                );
            
            return LocalRedirect(returnUrl);
        }

    }
}