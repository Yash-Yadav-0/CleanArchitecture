﻿using CleanArchitecture.Api.Controllers._BaseController;
using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.Commands.DeleteProduct;
using CleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using CleanArchitecture.Application.Features.Products.Queries.GetAllProducts;
using CleanArchitecture.Application.Features.Products.Queries.GetProductById;
using CleanArchitecture.Application.Features.Products.Queries.GetProductsFiltration;
using CleanArchitecture.Application.Features.Products.Queries.PutProductInExcel;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        public ProductsController(IMediator mediator) : base(mediator)
        {

        }

        [HttpGet("GetAllProducts")]
        public async Task<IList<GetAllProductQueryResponse>> GetAllProductsAsync()
        {
            return await mediator.Send(new GetAllProductQueryRequest());
        }

        [HttpGet("GetAllProductsInExcel")]
        public async Task GetAllProductsInExcelAsync() => await mediator.Send(new PutProductInExcelQueryRequest());

        [HttpGet("GetProductWithFilteration")]
        public async Task<List<GetProductsFilterationQueryResponse>> GetProductWithFilteration([FromQuery] GetProductsFilterationQueryRequest request)
            => await mediator.Send(request);

        [HttpGet("GetProductById/{id:int}")]
        public async Task<IList<GetProductByIdQueryResponse>> GetProductById(int id)
        {
            return await mediator.Send(new GetProductByIdQueryRequest() { ProductId = id });
        }
        [PermissionAuthorize(Permissions.ManageProducts)]
        [HttpPost("CreateProducts")]
        public async Task<IActionResult> CreateProductsAsync([FromForm] CreateProductCommandRequest request)
        {
            await mediator.Send(request);
            return CreatedAtAction(nameof(GetProductById), new { id = request.BrandId }, request);
            //return Created();
        }
        [PermissionAuthorize(Permissions.ManageProducts)]
        [HttpPut("UpdateProducts")]
        public async Task<IActionResult> UpdateProductsAsync([FromForm] UpdateProductCommandRequest request)
        {
            await mediator.Send(request);
            return Accepted();
            //return StatusCode(StatusCodes.Status202Accepted);
        }
        [PermissionAuthorize(Permissions.ManageProducts)]
        [HttpDelete("DeleteProducts")]
        public async Task DeleteProductsAsync([FromForm] DeleteProductCommandRequest request)
        {
            await mediator.Send(request);
        }
    }
}
