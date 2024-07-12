using CleanArchitecture.Application.Dtos;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductsFiltration
{
    public class GetProductsFilterationQueryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public BrandDTO Brand { get; set; }
        public IList<ImageDTO> Images { get; set; }
        public List<CategoriesOfProductsDTO> CategoriesOfProducts { get; set; }
    }
}
