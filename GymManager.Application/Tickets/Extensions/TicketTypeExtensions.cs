using GymManager.Application.Tickets.Queries.GetAddTicket;
using GymManager.Domain.Entities;


namespace GymManager.Application.Tickets.Extensions;

public static class TicketTypeExtensions
{
    public static TicketTypeDto ToDto(this TicketType ticket)
    {
        if (ticket == null)
            return null;

        return new TicketTypeDto
        {
            Id = ticket.Id,
            Price = ticket.Price,
            TicketTypeEnum = ticket.TicketTypeEnum,
            Name = ticket.Translations.FirstOrDefault()?.Name,
        };
    }
}
