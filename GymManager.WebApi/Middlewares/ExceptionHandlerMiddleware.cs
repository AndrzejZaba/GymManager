using Newtonsoft.Json;
using System.Net;

namespace GymManager.WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "GymManager Request: Nieobsłużony wyjątek - Request {Name}", context.Request.Path);

            await HandlerExceptionAsync(context, exception).ConfigureAwait(false);
        }
    }

    private Task HandlerExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        
        return context.Response.WriteAsJsonAsync(new { error = exception.Message });
    }
}
