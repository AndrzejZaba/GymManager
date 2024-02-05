using GymManager.Application.Announcements.Extensions;
using GymManager.Application.Announcements.Queries.Dtos;
using GymManager.Application.Common.Extensions;
using GymManager.Application.Common.Interfaces;
using GymManager.Application.Common.Models;
using GymManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Application.Clients.Queries.GetClientDashboard;

public class GetClientDashboardHandler : IRequestHandler<GetClientDashboardQuery, GetClientDashboardVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;

    public GetClientDashboardHandler(
        IApplicationDbContext context,
        IDateTimeService dateTimeService)
    {
        _context = context;
        _dateTimeService = dateTimeService;
    }

    public async Task<GetClientDashboardVm> Handle(GetClientDashboardQuery request, CancellationToken cancellationToken)
    {
        var vm = new GetClientDashboardVm();

        var user = await GetUser(request);

        vm.Email = user.Email;

        var ticket = GetActiveTicket(user);

        vm.TicketEndDate = GetTicketEndDate(ticket);

        vm.Announcements = await GetAnnouncements(request);

        return vm;
    }

    private static DateTime? GetTicketEndDate(Ticket ticket)
    {
        return ticket == null ? null : ticket.EndDate;
    }

    private Ticket GetActiveTicket(ApplicationUser user)
    {
        return user.Tickets
            .FirstOrDefault(x => x.StartDate.Date <= _dateTimeService.Now.Date &&
            x.EndDate >= _dateTimeService.Now.Date);
    }

    private async Task<ApplicationUser> GetUser(GetClientDashboardQuery request)
    {
        return await _context
            .Users
            .Include(x => x.Tickets)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.UserId);
    }

    private async Task<PaginatedList<AnnouncementDto>> GetAnnouncements(GetClientDashboardQuery request)
    {
        return await _context.Announcements
            .AsNoTracking()
            .OrderByDescending(x => x.Date)
            .Select(x => x.ToDto())
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
