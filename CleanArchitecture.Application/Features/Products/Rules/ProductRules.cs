using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.Products.Exceptions;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Features.Products.Rules
{
    public interface IProductRules 
    {
        public Task ProductsTitleMustNotBeTheSame(IList<Product> products, string TitleOfRequest);
    }
    public class ProductRules : IProductRules
    {
        public Task ProductsTitleMustNotBeTheSame(IList<Product> products, string titleOfRequest)
        {
            if (products.Any(t => t.Title == titleOfRequest))
                throw new ProductsTitleMustNotBeTheSameException();
            return Task.CompletedTask;
        }
    }
}
