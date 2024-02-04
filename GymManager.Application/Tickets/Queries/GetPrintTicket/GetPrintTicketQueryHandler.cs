using GymManager.Application.Common.Interfaces;
using GymManager.Application.Tickets.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Application.Tickets.Queries.GetPrintTicket;

public class GetPrintTicketQueryHandler : IRequestHandler<GetPrintTicketQuery, PrintTicketDto>
{
    private readonly IApplicationDbContext _context;

    public GetPrintTicketQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<PrintTicketDto> Handle(GetPrintTicketQuery request, CancellationToken cancellationToken)
    {
        var ticket = (await _context.Tickets
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == request.TicketId && x.UserId == request.UserId))
            .ToPrintTicketDto();

        return ticket;
    }
}
