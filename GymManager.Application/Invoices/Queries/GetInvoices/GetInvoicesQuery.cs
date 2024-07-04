using MediatR;

namespace GymManager.Application.Invoices.Queries.GetInvoices;

public class GetInvoicesQuery : IRequest<IEnumerable<InvoiceBasicsDto>>
{
    public string UserId { get; set; }  
}
