using Service.Orders.Models;
using System.Collections.Generic;

namespace Service.Orders.GetOrderByOrderId
{
    public class GetOrderByOrderIdServiceResponse
    {
        public string OrderId { get; set; }
        public decimal MinBinWidth { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; }
    }
}
