using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Application.Tickets.Commands.AddTicket;

public class AddTicketCommandHandler : IRequestHandler<AddTicketCommand>
{
    private readonly ILogger<AddTicketCommandHandler> _logger;

    public AddTicketCommandHandler(ILogger<AddTicketCommandHandler> logger)
    {
        _logger = logger;
    }
    public async Task<Unit> Handle(AddTicketCommand request, CancellationToken cancellationToken)
    {
        //var ticket = new Ticket();
        //ticket.Name = request.Name;

        //zapis do bazy danych
        
        return Unit.Value;
    }
}
