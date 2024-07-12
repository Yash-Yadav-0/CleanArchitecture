using CleanArchitecture.Api.Controllers._BaseController;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizationController : BaseController
    {
        public AuthorizationController(IMediator mediator) : base(mediator) { }
    }
}
