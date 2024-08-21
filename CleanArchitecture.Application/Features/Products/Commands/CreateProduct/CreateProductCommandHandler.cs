using CleanArchitecture.Application.Bases;
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
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : BaseHandler, IRequestHandler<CreateProductCommandRequest, Unit>
    {
        private readonly IProductRules productRules;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILocalStorage localStorage;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork,
                                           IProductRules productRules,
                                           IMapper mapper,
                                           IHttpContextAccessor httpContextAccessor,
                                           UserManager<User> userManager,
                                           ILocalStorage localStorage,
                                           RoleManager<Role> roleManager)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            this.productRules = productRules;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.localStorage = localStorage;
        }

        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

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

                if (!await IsUserInRoleAsync(userId, "Admin"))
                {
                    LoggerHelper.LogWarning("Unauthorized attempt to create product. UserId: {UserId} does not have Admin role.", userId);
                    throw new UnauthorizedAccessException("User does not have the required `Admin` role.");
                }

                var existingProducts = await UnitOfWork.readRepository<Product>().Find(p => p.Title == request.Title);

                if (existingProducts.Any())
                {
                    throw new ProductsTitleMustNotBeTheSameException();
                }

                var product = new Product(request.Title, request.Description, request.Price, request.Discount, request.BrandId);
                await UnitOfWork.writeRepository<Product>().AddAsync(product);

                foreach (var categoryId in request.CategortIds)
                {
                    await UnitOfWork.writeRepository<ProductsCategories>().AddAsync(new ProductsCategories
                    {
                        CategoryId = categoryId,
                        ProductId = product.Id
                    });
                }

                if (request.Images != null && request.Images.Any())
                {
                    IList<(string FileName, string Path)> uploadedFiles = await localStorage.UploadManyFilesAsync("Product",request.Images,cancellationToken);
                    if (uploadedFiles.Count > 0)
                    {
                        foreach (var (FileName, Path) in uploadedFiles)
                        {
                            product.images.Add(new Domain.Entities.Image
                            {
                                Path = Path,
                                FileName = FileName,
                                ProductId = product.Id
                            });
                        }
                    }
                }

                await UnitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Error occurred while handling CreateProductCommandRequest with Title: {Title}", ex, request.Title);
                throw;
            }

            return Unit.Value;
        }

        private async Task<bool> IsUserInRoleAsync(string userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user != null && await userManager.IsInRoleAsync(user, roleName);
        }
    }
}
