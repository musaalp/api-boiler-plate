using Sdk.Core.Entities;
using System.Collections.Generic;

namespace Data.Orders.GetOrderDetailsByOrderId
{
    public class GetOrderDetailsByOrderIdDataResponse
    {
        public string OrderId { get; set; }
        public decimal MinBinWidth { get; set; }
        public List<OrderDetailEntity> OrderDetails { get; set; }
    }
}
