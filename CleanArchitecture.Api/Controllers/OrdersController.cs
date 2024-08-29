using CleanArchitecture.Api.Controllers._BaseController;
using CleanArchitecture.Application.Features.Orders.Command.UpdateOrder;
using CleanArchitecture.Application.Features.Orders.Comments.DeleteOrder;
using CleanArchitecture.Application.Features.Orders.Comments.MakeOrder;
using CleanArchitecture.Application.Features.Orders.Queries.GetAllOrders;
using CleanArchitecture.Application.Features.Orders.Queries.GetAllOrdersForCurrentUser;
using CleanArchitecture.Application.Features.Orders.Queries.GetOrderById;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        public OrdersController(IMediator mediator) : base(mediator)
        {

        }
        [PermissionAuthorize(Permissions.ManageOrder)]
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllOrdersQueryRequest(), cancellationToken);
            return Ok(result);
        }

        [PermissionAuthorize(Permissions.ManageOrder)]
        [HttpGet("GetOrderById/{id:int}")]
        public async Task<IActionResult> GetOrderById(int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetOrderByIdQueryRequest { OrderId = id }, cancellationToken);
            return Ok(result);
        }

        [PermissionAuthorize(Permissions.ManageOrder)]
        [HttpGet("GetOrdersForCurrentUser")]
        public async Task<IActionResult> GetOrdersForCurrentUser(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllOrdersForCurrentUserQueryRequest(), cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] MakeOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await mediator.Send(request, cancellationToken);
            return StatusCode(StatusCodes.Status204NoContent); // No content since update is successful
        }

        [Authorize]
        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromBody] DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await mediator.Send(request, cancellationToken);
            return NoContent(); // No content since delete is successful
        }
    }
}
