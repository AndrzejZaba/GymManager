using MediatR;


namespace GymManager.Application.EmployeeEvents.Queries.GetEmployeeEvents;

public class GetEmployeeEventsQuery : IRequest<IEnumerable<EmployeeEventDto>>
{
}
