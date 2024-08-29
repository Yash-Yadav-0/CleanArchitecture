using CleanArchitecture.Api.Controllers._BaseController;
using CleanArchitecture.Application.Features.Auth.Commands.Login;
using CleanArchitecture.Application.Features.Auth.Commands.RefreshToken;
using CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.ConfirmResetPassword;
using CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.ISCodeForResetPassword;
using CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.SendForResetPassword;
using CleanArchitecture.Application.Features.Auth.Commands.Revoke.RevokeForAllUsers;
using CleanArchitecture.Application.Features.Auth.Commands.Revoke.RevokeForUser;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IMediator mediator) : base(mediator) { }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommandRequest request)
        {
            var demo = await mediator.Send(request);
            return Ok(demo);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginCommandRequest request)
        {
            LoginCommandResponse Demo = await mediator.Send(request);
            return Ok(Demo);
        }

        [HttpPost("SendForResetPassword")]
        public async Task<IActionResult> SendForResetPassword([FromForm] SendForResetPasswordCommandsRequest request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpPut("ISCodeForResetPasswordCorrect")]
        public async Task<IActionResult> ISCodeForResetPasswordCorrect([FromForm] ISCodeForResetPasswordCommandRequest request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ConfirmResetPasswordCommandRequest request)
        {
            return Ok(await mediator.Send(request));
        }

        [PermissionAuthorize(Permissions.ManageUsers)]
        [HttpDelete("RevokeSpecificUser")]
        public async Task<IActionResult> RevokeSpecificUser([FromForm] RevokeForUserCommandRequest request)
        {

            return Ok(await mediator.Send(request));
        }
        [PermissionAuthorize(Permissions.ManageUsers)]
        [HttpDelete("RevokeAllUser")]
        public async Task<IActionResult> RevokeAllUser(RevokeForAllUsersCommandRequest request)
        {

            return Ok(await mediator.Send(request));
        }

    }
}
