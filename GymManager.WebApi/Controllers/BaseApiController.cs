using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        private ISender _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();

        //TODO: uzupełnić
        protected string UserId => "286e6d55-3e1f-43e7-b6a5-89735a52138d";
    }
}
