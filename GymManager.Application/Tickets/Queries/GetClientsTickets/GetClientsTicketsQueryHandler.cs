﻿using GymManager.Application.Common.Extensions;
using GymManager.Application.Common.Interfaces;
using GymManager.Application.Common.Models;
using GymManager.Application.Tickets.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GymManager.Application.Tickets.Queries.GetClientsTickets;

public class GetClientsTicketsQueryHandler : IRequestHandler<GetClientsTicketsQuery, PaginatedList<TicketBasicsDto>>
{
    private readonly IApplicationDbContext _context;

    public GetClientsTicketsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<TicketBasicsDto>> Handle(GetClientsTicketsQuery request, CancellationToken cancellationToken)
    {
        var tickets = _context
            .Tickets
            .Where(x => x.UserId == request.UserId)
            .AsNoTracking();

        //if (!string.IsNullOrWhiteSpace(request.SearchValue))
        //    tickets = tickets.Where(x => x.Id.Contains(request.SearchValue));

        tickets = !string.IsNullOrWhiteSpace(request.OrderInfo) ?
            tickets = tickets.OrderBy(request.OrderInfo) :
            tickets = tickets.OrderByDescending(x => x.EndDate);

        var paginatedList = await tickets
            .Include(x => x.Invoice)
            .Select(x => x.ToBasicsDto())
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return paginatedList;

    }
}
