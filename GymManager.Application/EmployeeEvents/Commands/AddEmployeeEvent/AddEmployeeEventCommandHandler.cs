using GymManager.Application.Common.Interfaces;
using GymManager.Domain.Entities;
using MediatR;

namespace GymManager.Application.EmployeeEvents.Commands.AddEmployeeEvent;

public class AddEmployeeEventCommandHandler : IRequestHandler<AddEmployeeEventCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;

    public AddEmployeeEventCommandHandler(
        IApplicationDbContext context,
        IDateTimeService dateTimeService)
    {
        _context = context;
        _dateTimeService = dateTimeService;
    }
    public async Task<Unit> Handle(AddEmployeeEventCommand request, CancellationToken cancellationToken)
    {
        if (request.IsFullDay.GetValueOrDefault())
            request.End = null;

        var employeeEvent = new EmployeeEvent
        {
            End = request.End,
            Start = request.Start,
            IsFullDay = request.IsFullDay.GetValueOrDefault(),
            UserId = request.UserId,
            CreateDate = _dateTimeService.Now
        };

        _context.EmployeeEvents.Add(employeeEvent);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
