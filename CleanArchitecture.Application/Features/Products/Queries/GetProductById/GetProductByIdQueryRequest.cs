using MediatR;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<IList<GetProductByIdQueryResponse>>
    {
        public int ProductId { get; set; }
    }
}
