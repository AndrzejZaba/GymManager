using GymManager.Application.Common.Exceptions;
using GymManager.Application.Dictionaries;
using GymManager.Application.Files.Commands.UploadFile;
using GymManager.Application.Files.Queries.GetFiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.UI.Controllers;

[Authorize(Roles = RolesDict.Administrator)]
public class FileController : BaseController
{
    private readonly ILogger _logger;

    public FileController(ILogger<FileController> logger)
    {
        _logger = logger;
    }
    public async Task<IActionResult> Files()
    {
        return View(await Mediator.Send(new GetFilesQuery()));
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IEnumerable<IFormFile> files)
    {
        try
        {
            await Mediator.Send(
                new UploadFileCommand
                {
                    Files = files
                });

            return Json(new { success = true });
        }
        catch (ValidationException exception)
        {
            _logger.LogError(exception, null);

            return Json(new { success = false,
                message = string.Join(". ", exception.Errors.Select
                (x => string.Join(". ", x.Value.Select(y => y))))
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, null);

            return Json(new { success = false });
        }
    }
}
