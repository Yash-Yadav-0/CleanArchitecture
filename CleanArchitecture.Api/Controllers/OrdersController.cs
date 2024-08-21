using CleanArchitecture.Api.Controllers._BaseController;
using CleanArchitecture.Application.Features.Orders.Command.UpdateOrder;
using CleanArchitecture.Application.Features.Orders.Comments.DeleteOrder;
using CleanArchitecture.Application.Features.Orders.Comments.MakeOrder;
using CleanArchitecture.Application.Features.Orders.Queries.GetAllOrders;
using CleanArchitecture.Application.Features.Orders.Queries.GetAllOrdersForCurrentUser;
using CleanArchitecture.Application.Features.Orders.Queries.GetOrderById;
using CleanArchitecture.Application.Features.Products.Queries.GetProductById;
using DocumentFormat.OpenXml.Office2010.Excel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]/")]
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

        [HttpGet("GetOrderById{id:int}")]
        public async Task<IList<GetOrderByIdQueryResponse>> GetOrderById(int id)
        {
            return await mediator.Send(new GetOrderByIdQueryRequest() { OrderId = id});
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
            return Ok(await mediator.Send(request));
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
