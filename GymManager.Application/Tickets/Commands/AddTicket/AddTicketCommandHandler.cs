using GymManager.Application.Common.Interfaces;
using GymManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Application.Tickets.Commands.AddTicket;

public class AddTicketCommandHandler : IRequestHandler<AddTicketCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;

    public AddTicketCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService)
    {
        _context = context;
        _dateTimeService = dateTimeService;
    }
    public async Task<string> Handle(AddTicketCommand request, CancellationToken cancellationToken)
    {
        // identyfikator sesji
        var sessionId = Guid.NewGuid().ToString();

        //dodanie nowej transakcji przez system płatności
        var token = "123";

        await AddToDatabase(request, sessionId, token, cancellationToken);

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
