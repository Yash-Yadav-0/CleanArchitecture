using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductQueryHandler : 
            SqlFunctionHandler<GetAllProductQueryRequest,GetAllProductQueryResponse>,
            IRequestHandler<GetAllProductQueryRequest, IList<GetAllProductQueryResponse>>
    { 
        public GetAllProductQueryHandler(IConfiguration configuration)
            : base(configuration)
        {
        }
        public async Task<IList<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling GetAllProductQueryRequest.");

            var functionName = "get_all_products";
            var parameters = new Dictionary<string, object>();

            try
            {
                var result = await HandleAsync(
                    request,
                    functionName,
                    reader => new GetAllProductQueryResponse
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        Discount = reader.GetDecimal(3),
                        Brand = new BrandDTO
                        {
                            Id = reader.GetInt32(4),
                            Name = reader.GetString(5),
                        },
                        Images = reader.IsDBNull(6)
                            ? new List<ImageDTO>()
                            : reader.GetFieldValue<string[]>(6).Select(id => new ImageDTO { FileName = id }).ToList(),
                        CategoriesOfProducts = reader.IsDBNull(7)
                            ? new List<CategoriesOfProductsDTO>()
                            : reader.GetFieldValue<string[]>(7).Select(name => new CategoriesOfProductsDTO { Name = name }).ToList(),
                    },
                    cancellationToken,
                    parameters
                );

                LoggerHelper.LogInformation("Successfully retrieved all products.");
                return result;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Error occurred while retrieving all products.",ex);
                throw; // Re-throw the exception after logging it
            }
        }
    }
}
