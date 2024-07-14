using GymManager.Application.GymInvoices.Queries.GetPdfGymInvoice;

namespace GymManager.Application.Common.Interfaces;

public interface IGymInvoices
{
    Task AddInvoice(string ticketId);
    Task<InvoicePdfVm> GetPdfInvoice(int id);
}
