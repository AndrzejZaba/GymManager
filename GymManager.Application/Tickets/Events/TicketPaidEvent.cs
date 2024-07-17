using GymManager.Application.Common.Events;


namespace GymManager.Application.Tickets.Events;

public class TicketPaidEvent : IEvent
{
    public string TicketId { get; set; }
    public string UserId { get; set; }  
}
