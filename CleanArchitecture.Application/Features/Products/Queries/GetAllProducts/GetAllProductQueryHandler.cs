using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, IList<GetAllProductQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IList<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {

            var Products = await unitOfWork.readRepository<Product>().GetAllAsync(include: x => x.Include(x => x.Brand).Include(x => x.images).Include(x => x.ProductsCategory).ThenInclude(x => x.Category));//

            mapper.Map<BrandDTO, Brand>(Products.Select(x => x.Brand).ToList());
            mapper.Map<ImageDTO, Image>(Products.SelectMany(x => x.images).ToList());

            var productsDemo = mapper.Map<GetAllProductQueryResponse, Product>(Products);

            foreach (var item in productsDemo)
            {
                var x = Products.SelectMany(x => x.ProductsCategory.Where(y => y.ProductId == item.Id)).Select(q => q.Category.Name).ToList();
                item.CategoriesOfProducts = x.Select(x => new CategoriesOfProductsDTO() { Name = x }).ToList();
            }

            foreach (var item in productsDemo)
                item.Price -= (item.Price * (item.Discount / 100));

            return productsDemo;

        }
    }
}
