using GymManager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Application.Invoices.Commands.EditInvoice;

public class EditInvoiceCommandHandler : IRequestHandler<EditInvoiceCommand>
{
    private readonly IApplicationDbContext _context;

    public EditInvoiceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(EditInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _context
            .Invoices
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

        if (invoice == null)
            throw new Exception("Invoice NotFound");

        invoice.Value = request.Value;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}
