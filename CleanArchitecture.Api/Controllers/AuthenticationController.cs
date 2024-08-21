using CleanArchitecture.Api.Controllers._BaseController;
using CleanArchitecture.Application.Features.Auth.Commands.Login;
using CleanArchitecture.Application.Features.Auth.Commands.RefreshToken;
using CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.ConfirmResetPassword;
using CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.ISCodeForResetPassword;
using CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.SendForResetPassword;
using CleanArchitecture.Application.Features.Auth.Commands.Revoke.RevokeForAllUsers;
using CleanArchitecture.Application.Features.Auth.Commands.Revoke.RevokeForUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IMediator mediatR) : base(mediatR) { }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommandRequest request)
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
        public async Task<IActionResult> SendForResetPassword(SendForResetPasswordCommandsRequest request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpPut("ISCodeForResetPasswordCorrect")]
        public async Task<IActionResult> ISCodeForResetPasswordCorrect(ISCodeForResetPasswordCommandRequest request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ConfirmResetPasswordCommandRequest request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpDelete("RevokeSpacificUser")]
        public async Task<IActionResult> RevokeSpacificUser([FromForm] RevokeForUserCommandRequest request)
        {

            return Ok(await mediator.Send(request));
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("RevokeAllUser")]
        public async Task<IActionResult> RevokeAllUser(RevokeForAllUsersCommandRequest request)
        {

            return Ok(await mediator.Send(request));
        }

    }
}
