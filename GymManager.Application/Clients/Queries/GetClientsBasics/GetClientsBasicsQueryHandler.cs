using GymManager.Application.Common.Interfaces;
using GymManager.Application.Dictionaries;
using GymManager.Application.Users.Extensions;
using MediatR;

namespace GymManager.Application.Clients.Queries.GetClientsBasics;

public class GetClientsBasicsQueryHandler : IRequestHandler<GetClientsBasicsQuery, IEnumerable<ClientBasicsDto>>
{
    private readonly IUserRoleManagerService _userRoleManagerService;

    public GetClientsBasicsQueryHandler(IUserRoleManagerService userRoleManagerService)
    {
        _userRoleManagerService = userRoleManagerService;
    }
    public async Task<IEnumerable<ClientBasicsDto>> Handle(GetClientsBasicsQuery request, CancellationToken cancellationToken)
    {
        var clients = (await _userRoleManagerService
            .GetUsersInRoleAsync(RolesDict.Klient))
            .Select(x => x.ToClientBasicsDto());

        return clients;
    }
}
