using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Application.Tickets.Commands.AddTicket;

public class AddTicketCommandHandler : IRequestHandler<AddTicketCommand, string>
{
    public async Task<string> Handle(AddTicketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
