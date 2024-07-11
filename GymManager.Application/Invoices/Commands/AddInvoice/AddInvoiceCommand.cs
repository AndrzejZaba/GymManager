using MediatR;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Application.Invoices.Commands.AddInvoice;

public class AddInvoiceCommand : IRequest<int>
{
    [Required]
    public string TicketId { get; set; }
    public string UserId { get; set; }

}
