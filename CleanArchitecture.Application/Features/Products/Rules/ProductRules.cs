using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.Products.Exceptions;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Features.Products.Rules
{
    public class ProductRules : BaseRule
    {
        public Task ProductsTitleMustNotBeTheSame(IList<Product> products, string titleOfRequest)
        {
            if (products.Any(t => t.Title == titleOfRequest))
                throw new ProductsTitleMustNotBeTheSameException();
            return Task.CompletedTask;
        }
    }
}
