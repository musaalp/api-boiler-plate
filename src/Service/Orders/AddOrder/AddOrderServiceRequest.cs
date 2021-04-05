using MediatR;
using Service.Orders.Models;
using System.Collections.Generic;

namespace Service.Orders.AddOrder
{
    public class AddOrderServiceRequest : IRequest<AddOrderServiceResponse>
    {
        public string OrderId { get; set; }
        public string Email { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
