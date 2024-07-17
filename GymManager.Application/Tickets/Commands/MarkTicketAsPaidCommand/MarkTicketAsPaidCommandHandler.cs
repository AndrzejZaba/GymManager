using GymManager.Application.Common.Events;
using GymManager.Application.Common.Interfaces;
using GymManager.Application.Common.Models.Payments;
using GymManager.Application.Tickets.Events;
using GymManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Application.Tickets.Commands.MarkTicketAsPaidCommand;

public class MarkTicketAsPaidCommandHandler : IRequestHandler<MarkTicketAsPaidCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IPrzelewy24 _przelewy24;
    private readonly ILogger _logger;
    private readonly IEventDispatcher _eventDispatcher;

    public MarkTicketAsPaidCommandHandler(
        IApplicationDbContext context,
        IPrzelewy24 przelewy24,
        ILogger<MarkTicketAsPaidCommandHandler> logger,
        IEventDispatcher eventDispatcher)
    {
        _context = context;
        _przelewy24 = przelewy24;
        _logger = logger;
        _eventDispatcher = eventDispatcher;
    }
    public async Task<Unit> Handle(MarkTicketAsPaidCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Przelewy24 - payment verification started - {request.SessionId}");

        await VerifyTransactionPrzelewy24(request);

        var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.SessionId);

        await UpdatePaymentInDb(ticket, cancellationToken);

        _logger.LogInformation($"Przelewy24 - payment verification finished - {request.SessionId}");

        await _eventDispatcher.PublishAsync(new TicketPaidEvent
        {
            TicketId = ticket.Id,
            UserId = ticket.UserId
        });

        //await _gymInvoices.AddInvoice(ticket.Id);

        //await _userNotificationService.SendNotification(ticket.UserId, "Płatność została potwierdzona. Dziękujemy za zakup karnetu");

        return Unit.Value;
    
    }

    private async Task UpdatePaymentInDb(Ticket ticket, CancellationToken cancellationToken)
    {
        ticket.IsPaid = true;
        await _context.SaveChangesAsync(); 
    }

    private async Task VerifyTransactionPrzelewy24(MarkTicketAsPaidCommand request)
    {
        var response = await _przelewy24.TransactionVerifyAsync(new P24TransactionVerifyRequest
        {
            Amount = request.Amount,
            Currency = request.Currency,
            MerchantId = request.MerchantId,
            PosId = request.PosId,
            SessionId = request.SessionId,
        });

        if (response.Data?.Status != "success")
            throw new Exception("Invalid payment status in przelewy24");
    }
}
