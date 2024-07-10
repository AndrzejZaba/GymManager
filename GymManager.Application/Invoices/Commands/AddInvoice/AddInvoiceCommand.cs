using MediatR;


namespace GymManager.Application.Invoices.Commands.AddInvoice;

public class AddInvoiceCommand : IRequest<int>
{
    public string TicketId { get; set; }
    public string UserId { get; set; }

}
