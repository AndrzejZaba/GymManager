using DataTables.AspNet.Core;
using GymManager.Application.Clients.Queries.GetClient;
using GymManager.Application.Tickets.Commands.AddTicket;
using GymManager.Application.Tickets.Queries.GetAddTicket;
using GymManager.Application.Tickets.Queries.GetClientsTickets;
using GymManager.UI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.UI.Controllers
{
    [Authorize]
    public class TicketController : BaseController
    {
        private readonly IConfiguration _configuration;

        public TicketController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Tickets()
        {
            var isUserDataCompleted = !string.IsNullOrWhiteSpace((await Mediator.Send(new GetClientQuery { UserId = UserId })).FirstName);

            return View(isUserDataCompleted);
        }

        public async Task<IActionResult> TicketsDataTable(IDataTablesRequest request)
        {
            var tickets = await Mediator.Send(new GetClientsTicketsQuery
            {
                UserId = UserId,
                PageSize = request.Length,
                SearchValue = request.Search.Value,
                PageNumber = request.GetPageNumber(),
                OrderInfo = request.GetOrderInfo()
            });

            return request.GetResponse(tickets);
        }

        public async Task<IActionResult> AddTicket()
        {
            return View(await Mediator.Send(new GetAddTicketQuery())); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTicket(AddTicketVm vm)
        {
            var result = await MediatorSendValidate(new AddTicketCommand
            {
                StartDate = vm.Ticket.StartDate,
                TicketTypeId = vm.Ticket.TicketTypeId,
                UserId = UserId,
                Price = vm.Ticket.Price,
            });

            if (!result.IsValid)
                return View(vm);

            TempData["Success"] = "Nowy karnet został utworzony. Oczekiwanie na zweryfikowanie płatności.";

            // Dodanie karnetu z płatnością
            return Redirect($"{_configuration.GetValue<string>("Przelewy24:BaseUrl")}/trnRequest/{result.Model}");


            // Dodanie karnetu bez płatności
            //return RedirectToAction("Tickets");

        }

    }
}
