using CleanArchitecture.Application.Bases;

namespace CleanArchitecture.Application.Features.Products.Exceptions
{
    public class ProductsTitleMustNotBeTheSameException : BaseException
    {
        public ProductsTitleMustNotBeTheSameException() : base("This Title is already exist, try again with another title") { }
    }
}
