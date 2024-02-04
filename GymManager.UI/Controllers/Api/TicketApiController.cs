using GymManager.Application.Tickets.Commands.MarkTicketAsPaidCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.UI.Controllers.Api;

[Route("api/ticket")]
[ApiController]
public class TicketApiController : BaseApiController
{
    private readonly ILogger _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public TicketApiController(
        ILogger<TicketApiController> logger, 
        IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }

    [Route("updatestatus")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateStatus(MarkTicketAsPaidCommand command)
    {
		try
		{
            command.IsProduction = _webHostEnvironment.IsProduction();

            await Mediator.Send(command);
		}
		catch (Exception exception)
		{
            _logger.LogError(exception, null);
            return BadRequest();
		}
        return NoContent();
    }
}
