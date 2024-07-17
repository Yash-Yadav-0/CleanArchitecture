using CleanArchitecture.Api.Controllers._BaseController;
using CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToMember;
using CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToVendor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserActionController : BaseController
    {
        public UserActionController(IMediator mediator) : base(mediator) { }

        [HttpPost("ChangeToMember")]
        public async Task<IActionResult> ChangeToMember([FromForm] ChangeToMemberCommandRequest request, CancellationToken cancellationToken)
        {
            await mediator.Send(request);
            return Ok();
        }
        [HttpPost("ChangeToVendor")]
        public async Task<IActionResult> ChangeToVendor([FromForm] ChangeToVendorCommandRequest request,CancellationToken cancellationToken)
        {
            await mediator.Send(request);
            return Ok();
        }
    }
}
