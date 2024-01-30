

namespace GymManager.Application.Tickets.Queries.GetClientsTickets;

public class TicketBasicsDto
{
    public string Id { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public bool IsPaid { get; set; }
    public int? InvoiceId { get; set; }
}
