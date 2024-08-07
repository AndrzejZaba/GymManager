﻿
using MediatR;

namespace GymManager.Application.Tickets.Queries.GetAddTicket;

public class GetAddTicketQuery : IRequest<AddTicketVm>
{
    public string Language { get; set; }
}
