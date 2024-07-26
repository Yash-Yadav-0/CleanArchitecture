﻿using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler :
        SqlFunctionHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>,
        IRequestHandler<GetProductByIdQueryRequest, IList<GetProductByIdQueryResponse>>
    {
        public GetProductByIdQueryHandler(IConfiguration configuration)
            : base(configuration) { }

        public async Task<IList<GetProductByIdQueryResponse>> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var functionName = "get_product_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "productId", request.ProductId }
            };

            // Fetch the result using SqlFunctionHandler
            return await HandleAsync(
                request,
                functionName,
                reader => new GetProductByIdQueryResponse
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    Discount = reader.GetDecimal(4),
                    Brand = new BrandDTO
                    {
                        Id = reader.GetInt32(5),
                        Name = reader.GetString(6),
                    },
                    Images = reader.IsDBNull(7)
                        ? new List<ImageDTO>()
                        : reader.GetFieldValue<string[]>(7).Select(fileName => new ImageDTO { FileName = fileName }).ToList(),
                    CategoriesOfProducts = reader.IsDBNull(8)
                        ? new List<CategoriesOfProductsDTO>()
                        : reader.GetFieldValue<string[]>(8).Select(name => new CategoriesOfProductsDTO { Name = name }).ToList(),
                },
                cancellationToken,
                parameters
            );
        }
    }
}
