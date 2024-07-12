using GymManager.Application.Common.Interfaces;
using GymManager.Application.Common.Models;
using GymManager.Application.Invoices.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Application.Invoices.Queries.GetPdfInvoice;

public class GetPdfInvoiceQueryHandler : IRequestHandler<GetPdfInvoiceQuery, InvoicePdfVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IPdfFileGenerator _pdfFileGenerator;

    public GetPdfInvoiceQueryHandler(
        IApplicationDbContext context,
        IPdfFileGenerator pdfFileGenerator)
    {
        _context = context;
        _pdfFileGenerator = pdfFileGenerator;
    }
    public async Task<InvoicePdfVm> Handle(GetPdfInvoiceQuery request, CancellationToken cancellationToken)
    {
        var vm = new InvoicePdfVm();

        vm.Handle = Guid.NewGuid().ToString();

        var invoice = (await _context
            .Invoices
            .Include(x => x.Ticket).ThenInclude(x => x.TicketType)
            .Include(x => x.User).ThenInclude(x => x.Address)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.InvoiceId && x.UserId == request.UserId))
            .ToDto();

        if (invoice == null)
            return null;


        vm.PdfContent = await _pdfFileGenerator.GetAsync(new FileGeneratorParams
        {
            Context = request.Context,
            Model = invoice,
            ViewTemplate = "InvoicePreview"
        });

        vm.FileName = $"Faktura_{invoice.Title}.pdf";

        return vm;
    }
}
