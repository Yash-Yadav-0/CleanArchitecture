﻿using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using Irony.Parsing;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Errors.Model;
using System.Security.Claims;

namespace CleanArchitecture.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : BaseHandler, IRequestHandler<UpdateProductCommandRequest, Unit>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor,UserManager<User> userManager)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling UpdateProductCommandRequest for ProductId: {ProductId}", request.Id);

            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                LoggerHelper.LogWarning("HttpContext is null.");
                throw new UnauthorizedAccessException("HttpContext is not available.");
            }
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                LoggerHelper.LogWarning("Unauthorized attempt to update product. UserId is null.");
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null || !(await userManager.IsInRoleAsync(user, "Admin")))
            {
                LoggerHelper.LogWarning("Unauthorized attempt to update product. UserId: {UserId} does not have Admin role.", userId);
                throw new UnauthorizedAccessException("User does not have the required Admin role.");
            }

            var product = await UnitOfWork.readRepository<Product>().GetAsync(x => x.Id == request.Id);

            if (product == null)
            {
                LoggerHelper.LogWarning("Attempted to update non-existing product with ProductId: {ProductId}.", request.Id);
                throw new NotFoundException("Product not found.");
            }

            // Update the properties of the existing product
            product.Title = request.Title;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Discount = request.Discount;
            product.BrandId = request.BrandId;

            try
            {
                // Update categories if necessary
                IList<ProductsCategories> existingCategories = await UnitOfWork.readRepository<ProductsCategories>()
                    .GetAllAsync(p => p.ProductId == request.Id);

                await UnitOfWork.writeRepository<ProductsCategories>().DeleteRangeAsync(existingCategories);

                foreach (var categoryId in request.CategortIds)
                {
                    await UnitOfWork.writeRepository<ProductsCategories>().AddAsync(new ProductsCategories
                    {
                        CategoryId = categoryId,
                        ProductId = request.Id,
                    });
                }

                // Save changes
                await UnitOfWork.writeRepository<Product>().UpdateAsync(product.Id, product);
                await UnitOfWork.SaveChangeAsync();

                LoggerHelper.LogInformation("Product with ProductId: {ProductId} was successfully updated.", request.Id);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Error occurred while updating product with ProductId: {ProductId}.", ex, request.Id);
                throw; // Re-throw the exception after logging it
            }
        }
    }
}
