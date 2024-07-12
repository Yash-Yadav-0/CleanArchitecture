using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers._BaseController
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IMediator mediator;
        public BaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}
