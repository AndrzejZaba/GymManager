using GymManager.Application.Invoices.Queries.GetInvoice;
using GymManager.Application.Invoices.Queries.GetInvoices;
using GymManager.Domain.Entities;
using System.Linq.Expressions;

namespace GymManager.Application.Invoices.Extensions;

public static class InvoiceExtensions
{
    public static InvoiceBasicsDto ToBasicsDto(this Invoice invoice)
    {
        if (invoice == null)
            return null;

        return new InvoiceBasicsDto
        {
            Id = invoice.Id,
            UserId = invoice.UserId,
            CreatedDate = invoice.CreatedDate,
            Value = invoice.Value,
            Title = $"{invoice.Id}/{invoice.Month}/{invoice.Year}",
            UserName = $"{invoice.User.FirstName} {invoice.User.LastName}"
            
        };
    }

    public static InvoiceDto ToDto(this Invoice invoice)
    {
        if (invoice == null)
            return null;

        return new InvoiceDto
        {
            Id = invoice.Id,
            UserId = invoice.UserId,
            CreatedDate = invoice.CreatedDate,
            Value = invoice.Value,
            Title = $"{invoice.Id}/{invoice.Month}/{invoice.Year}",
            UserName = $"{invoice.User.FirstName} {invoice.User.LastName}",
            MethodOfPayment = invoice.MethodOfPayment,
            TicketId = invoice.TicketId,
            PositionName = invoice.Ticket.TicketType.TicketTypeEnum.ToString(),
            UserEmail = invoice.User.Email,
            UserCity = $"{invoice.User.Address.ZipCode} {invoice.User.Address.City}",
            UserStreet = $"{invoice.User.Address.Street} {invoice.User.Address.StreetNumber}"

        };
    }
}
