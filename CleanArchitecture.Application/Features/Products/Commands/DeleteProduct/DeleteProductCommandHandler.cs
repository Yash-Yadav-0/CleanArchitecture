using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Errors.Model;
using System.Security.Claims;

namespace CleanArchitecture.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : BaseHandler, IRequestHandler<DeleteProductCommandRequest, Unit>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        : base(unitOfWork, mapper, httpContextAccessor)
        {
            this.userManager = userManager;
        }
        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling DeleteProductCommandRequest for ProductId: {ProductId}", request.Id);

            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                LoggerHelper.LogWarning("Unauthorized attempt to delete product. UserId is null.");
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null || !(await userManager.IsInRoleAsync(user, "ADMIN")))
            {
                LoggerHelper.LogWarning("Unauthorized attempt to delete product. UserId: {UserId} does not have Admin role.", userId);
                throw new UnauthorizedAccessException("User does not have the required Admin role.");
            }

            Product product = await UnitOfWork.readRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (product == null)
            {
                LoggerHelper.LogWarning("Attempted to delete non-existing product with ProductId: {ProductId}.", request.Id);
                throw new NotFoundException("Product not found.");
            }

            product.IsDeleted = true;
            product.UpdatedDate = DateTime.UtcNow;

            try
            {
                await UnitOfWork.writeRepository<Product>().UpdateAsync(request.Id, product);
                await UnitOfWork.SaveChangeAsync();

                LoggerHelper.LogInformation("Product with ProductId: {ProductId} was successfully marked as deleted.", request.Id);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Error occurred while deleting product with ProductId: {ProductId}.", ex, request.Id);
                throw; // Re-throw the exception after logging it
            }
        }
    }
}
