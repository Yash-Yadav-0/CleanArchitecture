﻿using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductsFiltration
{
    public class GetProductsFilterationQueryHandler : IRequestHandler<GetProductsFilterationQueryRequest, List<GetProductsFilterationQueryResponse>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetProductsFilterationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<GetProductsFilterationQueryResponse>> Handle(GetProductsFilterationQueryRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling GetProductsFilterationQueryRequest with search input: {SearchInput}", request.SearchInput);

            var Products = await unitOfWork.readRepository<Product>()
                .GetAllAsync(include: x => x.Include(x => x.Brand)
                .Include(x => x.images)
                .Include(x => x.ProductsCategory)
                .ThenInclude(x => x.Category));//

            mapper.Map<BrandDTO, Brand>(Products.Select(x => x.Brand).ToList());
            mapper.Map<ImageDTO, Image>(Products.SelectMany(x => x.images).ToList());

            var productsDemo = mapper.Map<GetProductsFilterationQueryResponse, Product>(Products);

            foreach (var item in productsDemo)
            {
                var NameOfCategories = Products.SelectMany(x => x.ProductsCategory.Where(y => y.ProductId == item.Id)).Select(q => q.Category.Name).ToList();
                item.CategoriesOfProducts = NameOfCategories.Select(x => new CategoriesOfProductsDTO() { Name = x }).ToList();
            }

            foreach (var item in productsDemo)
                item.Price -= (item.Price * (item.Discount / 100));

            var TempData = productsDemo.Where(x => x.Title.Contains(request.SearchInput)).ToList();

            switch (request.OrderedField)
            {
                case OrderedField.Title:
                    TempData = TempData.OrderBy(x => x.Title).ToList();
                    break;
                case OrderedField.Price:
                    TempData = TempData.OrderBy(x => x.Price).ToList();
                    break;
                case OrderedField.Rank:
                    TempData = TempData.OrderBy(x => x.Discount).ToList();
                    break;
                default:
                    TempData = TempData;
                    break;
            }
            LoggerHelper.LogInformation("Successfully handled GetProductsFilterationQueryRequest. Filtered products count: {Count}", TempData.Count);

            return TempData;
        }
    }
}
