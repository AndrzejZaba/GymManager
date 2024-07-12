

namespace GymManager.Application.Invoices.Queries.GetPdfInvoice;

public class InvoicePdfVm
{
    public string Handle { get; set; }
    public string FileName { get; set; }
    public byte[] PdfContent { get; set; }
}
