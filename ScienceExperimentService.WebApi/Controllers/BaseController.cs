using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ScienceExperimentService.WebApi.Controllers
{
    [ApiController]
    [Route("science/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
