using GymManager.Application.GymInvoices.Queries.GetPdfGymInvoice;

namespace GymManager.Application.Common.Interfaces;

public interface IGymInvoices
{
    Task AddInvoice(string ticketId, string userId = null);
    Task<InvoicePdfVm> GetPdfInvoice(int id);
}
