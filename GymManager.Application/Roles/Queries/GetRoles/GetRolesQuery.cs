using MediatR;

namespace GymManager.Application.Roles.Queries.GetRoles;

public class GetRolesQuery : IRequest<IEnumerable<RoleDto>>
{
}
