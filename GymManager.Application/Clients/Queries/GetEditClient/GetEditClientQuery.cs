using GymManager.Application.Clients.Commands.EditClient;
using MediatR;

namespace GymManager.Application.Clients.Queries.GetEditClient;

public class GetEditClientQuery : IRequest<EditClientCommand>
{
    public string UserId { get; set; }
}
