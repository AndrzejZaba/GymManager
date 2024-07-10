using GymManager.Application.Common.Interfaces;
using GymManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Application.Invoices.Commands.AddInvoice;

public class AddInvoiceCommandHandler : IRequestHandler<AddInvoiceCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;

    public AddInvoiceCommandHandler(
        IApplicationDbContext context,
        IDateTimeService dateTimeService)
    {
        _context = context;
        _dateTimeService = dateTimeService;
    }
    public async Task<int> Handle(AddInvoiceCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context
            .Tickets
            .FirstOrDefaultAsync(x => x.Id == request.TicketId);

        var invoice = new Invoice
        {
            CreatedDate = _dateTimeService.Now,
            MethodOfPayment = "Przelew",
            Month = _dateTimeService.Now.Month,
            Year = _dateTimeService.Now.Year,
            UserId = request.UserId,
            Value = ticket.Price,
            TicketId = request.TicketId
        };

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync(cancellationToken);

        return invoice.Id;
    }
}
