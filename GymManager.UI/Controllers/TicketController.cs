using DataTables.AspNet.Core;
using GymManager.Application.Clients.Queries.GetClient;
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

    }
}
