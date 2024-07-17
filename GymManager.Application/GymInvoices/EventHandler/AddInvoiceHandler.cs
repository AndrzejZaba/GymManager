using GymManager.Application.Common.Events;
using GymManager.Application.Common.Interfaces;
using GymManager.Application.Tickets.Events;

namespace GymManager.Application.GymInvoices.EventHandler;

public class AddInvoiceHandler : IEventHandler<TicketPaidEvent>
{
    private readonly IGymInvoices _gymInvoices;

    public AddInvoiceHandler(IGymInvoices gymInvoices)
    {
        _gymInvoices = gymInvoices;
    }
    public async Task HandleAsync(TicketPaidEvent @event)
    {
        await _gymInvoices.AddInvoice(@event.TicketId, @event.UserId);
    }
}
