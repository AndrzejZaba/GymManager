using GymManager.Application.Common.Interfaces;
using MediatR;

namespace GymManager.Application.Roles.Queries.GetRoles;

// IRequestHandler<komenda, typ który zwracamy> 
public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleDto>>
{
    private readonly IRoleManagerService _roleManagerService;

    public GetRolesQueryHandler(IRoleManagerService roleManagerService)
    {
        _roleManagerService = roleManagerService;
    }
    public async Task<IEnumerable<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return _roleManagerService.GetRoles();
    }
}
