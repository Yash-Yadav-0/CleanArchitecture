using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.Products.Rules;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.Storage;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CleanArchitecture.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : BaseHandler, IRequestHandler<CreateProductCommandRequest, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ProductRules productRules;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILocalStorage localStorage;
        private readonly UserManager<User> userManager;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork,
                                           ProductRules productRules,
                                           IMapper mapper,
                                           IHttpContextAccessor httpContextAccessor,
                                           UserManager<User> userManager,
                                           ILocalStorage localStorage

                                           )
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.productRules = productRules;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.localStorage = localStorage;
        }
        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
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

            IList<Product> products = await UnitOfWork.readRepository<Product>().GetAllAsync();

            await this.productRules.ProductsTitleMustNotBeTheSame(products, request.Title);

            Product product = new(request.Title, request.Description, request.Price, request.Discount, request.BrandId);

            await UnitOfWork.writeRepository<Product>().AddAsync(product);

            if (await UnitOfWork.SaveChangeAsync() > 0)
            {
                foreach (var categoryid in request.CategortIds)
                {
                    await UnitOfWork.writeRepository<ProductsCategories>()
                                                        .AddAsync(new ProductsCategories
                                                        {
                                                            CategoryId = categoryid,
                                                            ProductId = product.Id
                                                        });

                }
            }
            if (request.Images != null)
            {
                IList<(string fileName, string Path)> list = await localStorage.UploadManyAsync(product.Id, "images", request.Images);
                if (list != null)
                    foreach (var photo in list)
                        product.images.Add(new Domain.Entities.Image()
                        {
                            Path = photo.Path,
                            FileName = photo.fileName,
                            ProductId = product.Id,
                        });

            }

            await UnitOfWork.SaveChangeAsync();
            return Unit.Value;
        }
    }
}
