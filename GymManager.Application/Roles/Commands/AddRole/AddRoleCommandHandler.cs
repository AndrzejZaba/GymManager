using GymManager.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Application.Roles.Commands.AddRole;

public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand>
{
    private readonly IRoleManagerService _roleManagerService;

    public AddRoleCommandHandler(IRoleManagerService roleManagerService)
    {
        _roleManagerService = roleManagerService;
    }
    public async Task<Unit> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        await _roleManagerService.CreateAsync(request.Name);

        return Unit.Value;
    }
}
