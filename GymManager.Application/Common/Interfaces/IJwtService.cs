using GymManager.Application.Common.Models.Invoices;

namespace GymManager.Application.Common.Interfaces;

public interface IJwtService
{
    AuthenticateResponse GenerateJwtToken(string userId);
}
