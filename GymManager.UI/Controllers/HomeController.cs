﻿using AspNetCore.ReCaptcha;
using GymManager.Application.Common.Exceptions;
using GymManager.Application.Contacts.Commands.SendContactEmail;
using GymManager.Application.Tickets.Commands.AddTicket;
using GymManager.Application.Tickets.Queries.GetTicketById;
using GymManager.UI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymManager.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

    }
}