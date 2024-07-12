using MediatR;

namespace CleanArchitecture.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
