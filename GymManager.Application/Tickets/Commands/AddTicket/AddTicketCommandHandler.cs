using GymManager.Application.Common.Interfaces;
using GymManager.Application.Common.Models.Payments;
using GymManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Application.Tickets.Commands.AddTicket;

public class AddTicketCommandHandler : IRequestHandler<AddTicketCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly IPrzelewy24 _przelewy24;
    private readonly IHttpContext _httpContext;
    private readonly IGymInvoices _gymInvoices;

    public AddTicketCommandHandler(
        IApplicationDbContext context, 
        IDateTimeService dateTimeService, 
        IPrzelewy24 przelewy24,
        IHttpContext httpContext,
        IGymInvoices gymInvoices)
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _przelewy24 = przelewy24;
        _httpContext = httpContext;
        _gymInvoices = gymInvoices;
    }
    public async Task<string> Handle(AddTicketCommand request, CancellationToken cancellationToken)
    {
        // identyfikator sesji
        var sessionId = Guid.NewGuid().ToString();

        //dodanie nowej transakcji przez system płatności
        //var token = await AddTransactionPrzelewy24(request, sessionId);
        var token = "123";

        await AddToDatabase(request, sessionId, token, cancellationToken);

        return token;
    }

    private async Task<string> AddTransactionPrzelewy24(AddTicketCommand request, string sessionId)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Select(x => new { Email = x.Email, Id = x.Id })
            .FirstOrDefaultAsync(x => x.Id == request.UserId);

        var clientEmail = user.Email;

        var response = await _przelewy24.NewTransactionAsync(new P24TransactionRequest
        {
            Amount = (int)(request.Price * 100),
            Country = "PL",
            Currency = "PLN",
            Description = "Karnet",
            Email = clientEmail,
            Language = "pl",
            UrlReturn = $"{_httpContext.AppBaseUrl}/Ticket/Tickets",
            UrlStatus = $"{_httpContext.AppBaseUrl}/api/ticket/updatestatus",
            SessionId = sessionId
        });

        var token = string.Empty;

        if (string.IsNullOrWhiteSpace(response.Data?.Token))
            throw new Exception($"Przelewy24 haven't return token. Error: {response.Error}");
        else
            token = response.Data?.Token;

        return token;

    }

    private async Task AddToDatabase(AddTicketCommand request, string sessionId, string token, CancellationToken cancellationToken)
    {
        var ticketType = _context.TicketTypes
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == request.TicketTypeId);

        var ticket = new Ticket
        {
            TicketTypeId = request.TicketTypeId,
            StartDate = request.StartDate,
            UserId = request.UserId,
            Price = ticketType.Price,
            Id = sessionId,
            Token = token,
            CreatedDate = _dateTimeService.Now
        };

        switch (ticketType.TicketTypeEnum)
        {
            case Domain.Enums.TicketTypeEnum.Single:
                ticket.EndDate = request.StartDate.Date;
                break;
            case Domain.Enums.TicketTypeEnum.Weekly:
                ticket.EndDate = request.StartDate.AddDays(6).Date;
                break;
            case Domain.Enums.TicketTypeEnum.Monthly:
                ticket.EndDate = request.StartDate.AddDays(DateTime.DaysInMonth
                    (request.StartDate.Year, request.StartDate.Month) - 1).Date;
                break;
            case Domain.Enums.TicketTypeEnum.Annual:
                ticket.EndDate = request.StartDate.AddDays(364).Date;
                break;
            default:
                break;
        }

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
