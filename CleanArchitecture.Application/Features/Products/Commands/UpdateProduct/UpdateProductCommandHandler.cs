using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        }
        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            // Find the user and check if they are in the Admin role
            var user = await userManager.FindByIdAsync(userId);
            if (user == null || !(await userManager.IsInRoleAsync(user, "ADMIN")))
            {
                throw new UnauthorizedAccessException("User does not have the required Admin role.");
            }

            Product product = await UnitOfWork.readRepository<Product>().GetAsync(x => x.Id == request.Id);

            IList<ProductsCategories> _productsCategory = await UnitOfWork.readRepository<ProductsCategories>()
            .GetAllAsync(p => p.ProductId == request.Id);

            Product mapping = Mapper.Map<Product, UpdateProductCommandRequest>(request);

            mapping.UpdatedDate = DateTime.UtcNow;
            mapping.AddedOnDate = product.AddedOnDate;

            await UnitOfWork.writeRepository<ProductsCategories>().DeleteRangeAsync(_productsCategory);

            foreach (var categoryid in request.CategortIds)
            {
                await UnitOfWork.writeRepository<ProductsCategories>().AddAsync(new()
                {
                    CategoryId = categoryid,
                    ProductId = request.Id
                });
            }
            await UnitOfWork.writeRepository<Product>().UpdateAsync(product.Id, mapping);
            await UnitOfWork.SaveChangeAsync();

            return Unit.Value;
        }
    }
}
