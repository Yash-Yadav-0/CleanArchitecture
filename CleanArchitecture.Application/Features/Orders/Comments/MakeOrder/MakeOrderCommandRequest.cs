using CleanArchitecture.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Orders.Comments.MakeOrder
{
    public class MakeOrderCommandRequest
    {
        public IList<MakeOrderDTO> makeOrderDTOs { get; set; }
    }
}
