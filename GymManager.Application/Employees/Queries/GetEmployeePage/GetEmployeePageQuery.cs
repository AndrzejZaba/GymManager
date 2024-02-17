using MediatR;

namespace GymManager.Application.Employees.Queries.GetEmployeePage;

public class GetEmployeePageQuery : IRequest<EmployeePageDto>
{
    public string Url { get; set; }
}
