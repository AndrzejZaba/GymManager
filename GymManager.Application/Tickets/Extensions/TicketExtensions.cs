

using GymManager.Application.Tickets.Queries.GetClientsTickets;
using GymManager.Application.Tickets.Queries.GetPrintTicket;
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
    
    public static PrintTicketDto ToPrintTicketDto(this Ticket ticket)
    {
        if (ticket == null)
            return null;

        return new PrintTicketDto
        {
            QrCodeId = ticket.Id,
            EndDate = ticket.EndDate,
            StartDate = ticket.StartDate,
            CompanyContactEmail = "andzab00@gmail.com",
            CompanyContactPhone = "500 500 500",
            FullName = $"{ticket.User.FirstName} {ticket.User.LastName}",
            Image = "images/gym-logo.jpg"

        };
    } 
}
