using MediatR;
using Sdk.Core.Entities;
using System.Collections.Generic;

namespace Data.Orders.AddOrder
{
    public class AddOrderDataRequest : IRequest<bool>
    {
        public OrderEntity Order { get; set; }

        public List<OrderDetailEntity> OrderDetails { get; set; }
    }
}
