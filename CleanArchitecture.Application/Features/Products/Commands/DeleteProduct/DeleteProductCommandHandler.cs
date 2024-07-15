using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : BaseHandler, IRequestHandler<DeleteProductCommandRequest, Unit>
    {
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        : base(unitOfWork, mapper, httpContextAccessor)
        {
        }
        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product product = await UnitOfWork.readRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            product.IsDeleted = true;

            product.UpdatedDate = DateTime.UtcNow;
            product.AddedOnDate = product.AddedOnDate;

            await UnitOfWork.writeRepository<Product>().UpdateAsync(request.Id, product);
            await UnitOfWork.SaveChangeAsync();

            return Unit.Value;
        }
    }
}
