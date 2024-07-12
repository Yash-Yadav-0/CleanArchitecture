using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;
        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)

        {
            this.UnitOfWork = unitOfWork;
            this.Mapper = mapper;
        }
        async Task<GetProductByIdQueryResponse> IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>.Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {

            Product product = await UnitOfWork.readRepository<Product>()
                    .GetAsync(predicate: x => x.Id == request.ProductId);
                        //include: x => x.Include(x => x.Brand).Include(x => x.images).Include(x => x.ProductsCategory).ThenInclude(x => x.Category)
            Mapper.Map<ImageDTO, Image>(product.images.Where(x => x.ProductId == request.ProductId).ToList());
            Mapper.Map<BrandDTO, Brand>(product.Brand);

            //product.ProductsCategory.Select(x => x.Category).Where(obj => product.ProductsCategory.Any(x => x.ProductId == request.ProductId)).Select(X=>X.Name).ToList()  ;
            var ProcessedData = Mapper.Map<CategoriesOfProductsDTO, Category>(product.ProductsCategory.Select(x => x.Category).Where(obj => product.ProductsCategory.Any(x => x.ProductId == request.ProductId)).ToList());

            GetProductByIdQueryResponse queryResponse = Mapper.Map<GetProductByIdQueryResponse, Product>(product);

            queryResponse.CategoriesOfProducts = ProcessedData.ToList();

            return queryResponse;
        }
    }
}
