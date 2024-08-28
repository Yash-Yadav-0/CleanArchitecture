﻿using CleanArchitecture.Api.Controllers._BaseController;
using CleanArchitecture.Application.Features.Auth.Queries.GetAllUsers;
using CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToAdmin;
using CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToMember;
using CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToVendor;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]/")]
    [Authorize(Roles ="Admin")]
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
        [HttpPost("ChangeTOAdmin")]
        public async Task<IActionResult> ChangeToAdmin([FromForm] ChangeToAdminCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllUsersQueryRequest(), cancellationToken);
            return Ok(result);
        }
    }
}
