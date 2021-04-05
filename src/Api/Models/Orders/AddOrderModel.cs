using Service.Orders.Models;
using System.Collections.Generic;

namespace Api.Models.Orders
{
    public class AddOrderModel
    {
        public string OrderId { get; set; }
        public string Email { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
