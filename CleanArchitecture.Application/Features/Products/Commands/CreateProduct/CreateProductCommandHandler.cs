﻿using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.Products.Exceptions;
using CleanArchitecture.Application.Features.Products.Rules;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.Storage;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CleanArchitecture.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : BaseHandler, IRequestHandler<CreateProductCommandRequest, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ProductRules productRules;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILocalStorage localStorage;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork,
                                           ProductRules productRules,
                                           IMapper mapper,
                                           IHttpContextAccessor httpContextAccessor,
                                           UserManager<User> userManager,
                                           ILocalStorage localStorage,
                                           RoleManager<Role> roleManager

                                           )
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.productRules = productRules;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.localStorage = localStorage;
        }
        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling CreateProductCommandRequest with Title: {Title}", request.Title);
            try
            {
                var httpContext = httpContextAccessor.HttpContext;
                if (httpContext == null)
                {
                    LoggerHelper.LogWarning("HttpContext is null.");
                    throw new UnauthorizedAccessException("HttpContext is not available.");
                }

                var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    LoggerHelper.LogWarning("Unauthorized attempt to create products. UserId is null.");
                    throw new UnauthorizedAccessException("User is not authenticated.");
                }

                // Find the user and check if they are in the Admin role
                var user = await userManager.FindByIdAsync(userId);
                if (user == null || !(await userManager.IsInRoleAsync(user, "ADMIN")))
                {
                    LoggerHelper.LogWarning("Unauthorized attempt to create product. UserId: {UserId} does not have Admin role.", userId);
                    throw new UnauthorizedAccessException("User does not have the required `Admin` role.");
                }

                IQueryable<Product> queriedProduct = await UnitOfWork.readRepository<Product>().Find(p => p.Title == request.Title);

                IList<Product> existingProducts = await queriedProduct.ToListAsync();

                if (existingProducts.Any())
                {
                    throw new ProductsTitleMustNotBeTheSameException();
                }

                Product product = new(request.Title, request.Description, request.Price, request.Discount, request.BrandId);
                await UnitOfWork.writeRepository<Product>().AddAsync(product);


                if (await UnitOfWork.SaveChangeAsync() > 0)
                {
                    foreach (var categoryid in request.CategortIds)
                    {
                        await UnitOfWork.writeRepository<ProductsCategories>()
                                                            .AddAsync(new ProductsCategories
                                                            {
                                                                CategoryId = categoryid,
                                                                ProductId = product.Id
                                                            });

                    }
                }
                if (request.Images != null && request.Images.Any())
                {
                    IList<(string fileName, string Path)> list = await localStorage.UploadManyAsync(product.Id, "images", request.Images);
                    if (list != null)
                        foreach (var photo in list)
                            product.images.Add(new Domain.Entities.Image()
                            {
                                Path = photo.Path,
                                FileName = photo.fileName,
                                ProductId = product.Id,
                            });

                }
                await UnitOfWork.SaveChangeAsync();
                LoggerHelper.LogInformation("Successfully created product with Id: {productId} ",product.Id);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Error occurred while handling CreateProductCommandRequest with Title: {Title}", ex, request.Title);
                throw;
            }
        }
    }
}
