using GymManager.Application.Common.Interfaces;
using GymManager.Application.Common.Models.Invoices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace GymManager.Application.Users.Queries.GetUserToken;

public class GetUserTokenQueryHandler : IRequestHandler<GetUserTokenQuery, AuthenticateResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwtService;

    public GetUserTokenQueryHandler(IApplicationDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }
    public async Task<AuthenticateResponse> Handle(GetUserTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _context
            .Users
            .Select(x => new { x.Id, x.UserName, x.PasswordHash })
            .FirstOrDefaultAsync(x =>
            x.UserName == request.UserName &&
            x.PasswordHash == request.Password);

        if (user == null)
            return null;

        return _jwtService.GenerateJwtToken(user.Id);

    }
}
