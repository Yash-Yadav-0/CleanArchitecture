using MediatR;

namespace CleanArchitecture.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductQueryRequest : IRequest<IList<GetAllProductQueryResponse>>
    {

    }
}
