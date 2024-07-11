using Microsoft.AspNetCore.Http;

namespace GymManager.Application.Common.Interfaces;

public interface IHttpContext
{
    string AppBaseUrl { get; }
    string IpAddress { get; }
    public ISession Session { get; }
}
