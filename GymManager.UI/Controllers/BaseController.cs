using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace GymManager.UI.Controllers;

public abstract class BaseController : Controller
{

    private ISender _mediatr;
    protected ISender Mediatr => _mediatr ??= HttpContext.RequestServices.GetService<ISender>();
}
