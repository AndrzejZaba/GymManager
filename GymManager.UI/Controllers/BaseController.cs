using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace GymManager.UI.Controllers;

public abstract class BaseController : Controller
{

    private ISender _mediatr;
    protected ISender Mediator => _mediatr ??= HttpContext.RequestServices.GetService<ISender>();
}
