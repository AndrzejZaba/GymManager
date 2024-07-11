using GymManager.Application.Common.Interfaces;
using GymManager.Application.Common.Models.Invoices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GymManager.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly IDateTimeService _dateTimeService;

    public JwtService(IConfiguration configuration,
        IDateTimeService dateTimeService)
    {
        _configuration = configuration;
        _dateTimeService = dateTimeService;
    }
    public AuthenticateResponse GenerateJwtToken(string userId)
    {
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Secret").Value);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var authSigningKey = new SymmetricSecurityKey(key);

        var token = new JwtSecurityToken(
            expires: _dateTimeService.Now.AddDays(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return new AuthenticateResponse { Token = new JwtSecurityTokenHandler().WriteToken(token) };


    }
}
