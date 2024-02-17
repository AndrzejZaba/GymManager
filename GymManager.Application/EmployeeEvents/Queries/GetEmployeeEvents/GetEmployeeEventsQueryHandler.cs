using GymManager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Application.EmployeeEvents.Queries.GetEmployeeEvents;

public class GetEmployeeEventsQueryHandler : IRequestHandler<GetEmployeeEventsQuery, IEnumerable<EmployeeEventDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IRandomService _randomService;

    public GetEmployeeEventsQueryHandler(
        IApplicationDbContext context,
        IRandomService randomService)
    {
        _context = context;
        _randomService = randomService;
    }
    public async Task<IEnumerable<EmployeeEventDto>> Handle(GetEmployeeEventsQuery request, CancellationToken cancellationToken)
    {
        var employeeEvents = await _context.EmployeeEvents
            .Include(x => x.User)
            .AsNoTracking()
            .Select(x => new EmployeeEventDto
            {
                Id = x.Id,
                Start = x.Start,
                End = x.End,
                IsFullDay = x.IsFullDay,
                Title = $"{x.User.FirstName} {x.User.LastName}",
                UserId = x.UserId,
            })
            .ToListAsync();

        SetColors(employeeEvents);

        return employeeEvents;
    }

    private void SetColors(List<EmployeeEventDto> employeeEvents)
    {
        foreach (var employeeEvent in employeeEvents) 
            employeeEvent.ThemeColor = _randomService.GetColor();
        
    }
}
