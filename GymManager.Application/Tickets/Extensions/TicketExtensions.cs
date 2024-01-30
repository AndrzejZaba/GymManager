

using GymManager.Application.Tickets.Queries.GetClientsTickets;
using GymManager.Domain.Entities;

namespace GymManager.Application.Tickets.Extensions;

public static class TicketExtensions
{
    public static TicketBasicsDto ToBasicsDto(this Ticket ticket)
    {
        if (ticket == null)
            return null;

        return new TicketBasicsDto
        {
            EndDate = ticket.EndDate.ToLongDateString(),
            StartDate = ticket.StartDate.ToLongDateString(),
            IsPaid = ticket.IsPaid,
            InvoiceId = ticket.Invoice?.Id,
            Id = ticket.Id
        };
    } 
}
