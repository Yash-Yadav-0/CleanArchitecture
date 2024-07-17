using MediatR;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductsFiltration
{
    public enum OrderedField
    {
        [EnumMember(Value = "None")]
        None = 0,

        [EnumMember(Value = "Title")]
        Title = 1,

        [EnumMember(Value = "Price")]
        Price = 3,

        [EnumMember(Value = "Rank")]
        Rank = 5
    }
    public class GetProductsFilterationQueryRequest : IRequest<List<GetProductsFilterationQueryResponse>>
    {
        public string CacheKey => "GetProductsFilteration";
        public double CacheTime => 6;
        public string SearchInput { get; set; }

        [DefaultValue(OrderedField.None)]
        public OrderedField OrderedField { get; set; }
    }
}
