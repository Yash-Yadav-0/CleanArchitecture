using CleanArchitecture.Api.Controllers._BaseController;
using CleanArchitecture.Application.Features.Orders.Command.UpdateOrder;
using CleanArchitecture.Application.Features.Orders.Comments.DeleteOrder;
using CleanArchitecture.Application.Features.Orders.Comments.MakeOrder;
using CleanArchitecture.Application.Features.Orders.Queries.GetAllOrders;
using CleanArchitecture.Application.Features.Orders.Queries.GetAllOrdersForCurrentUser;
using CleanArchitecture.Application.Features.Orders.Queries.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        public OrdersController(IMediator mediator) : base(mediator)
        {

        }

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders() => Ok(await mediator.Send(new GetAllOrdersQueryRequest()));

        //[HttpGet("GetOrderById")]
        //public async Task<IActionResult> GetOrderById(GetOrderByIdQueryRequest request) => Ok(await mediator.Send(request));

        [HttpGet("GetOrderById")]
        public async Task<IActionResult> GetOrderById([FromForm] GetOrderByIdQueryRequest request)
        {
            var x = await mediator.Send(request);
            return Ok(x);
        }

        [Authorize]
        [HttpGet("GetOrdersForCurrentUser")]
        public async Task<IList<GetAllOrdersForCurrentUserQueryResponse>> GetOrdersForCurrentUser()
        {
            return await mediator.Send(new GetAllOrdersForCurrentUserQueryRequest());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(MakeOrderCommandRequest request)
        {
            await mediator.Send(request);
            return Created();
        }

        [Authorize]
        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderCommandRequest request)
        {
            await mediator.Send(request);
            return Created();
        }

        [Authorize]
        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromForm] DeleteOrderCommandRequest request) => Ok(await mediator.Send(request));

    }
}
