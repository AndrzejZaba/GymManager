using MediatR;
using Microsoft.AspNetCore.Mvc;
using GymManager.UI.Models;
using GymManager.Application.Common.Exceptions;
using System.Security.Claims;
using System.Globalization;

namespace GymManager.UI.Controllers;

public abstract class BaseController : Controller
{

    private ISender _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    protected string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
    protected string CurrentLanguage => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

    protected async Task<MediatorValidateResponse<T>> MediatorSendValidate<T>
        (IRequest<T> request)
    {
        var response = new MediatorValidateResponse<T> { IsValid = false };

        try
        {
            if (ModelState.IsValid)
            {
                response.Model = await Mediator.Send(request);
                response.IsValid = true;
                return response;
            }
        }
        catch (ValidationException exception)
        {
            foreach (var item in exception.Errors)
            {
                ModelState.AddModelError(item.Key, string.Join(". ",
                    item.Value));
            }
        }

        return response;
    }
}
