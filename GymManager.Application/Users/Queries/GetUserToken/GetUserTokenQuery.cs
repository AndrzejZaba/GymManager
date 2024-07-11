using GymManager.Application.Common.Models.Invoices;
using MediatR;


namespace GymManager.Application.Users.Queries.GetUserToken;

public class GetUserTokenQuery : IRequest<AuthenticateResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
