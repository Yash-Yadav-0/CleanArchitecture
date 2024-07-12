﻿using CleanArchitecture.Api.Controllers._BaseController;
using CleanArchitecture.Application.Features.Auth.Commands.Registration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApplicationUserController : BaseController
    {
        public ApplicationUserController(IMediator mediator) : base(mediator) { }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromForm] RegistrationCommandRequest request)
        {
            await mediator.Send(request);
            return Created();
        }
    }
}
