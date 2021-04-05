using MediatR;

namespace Data.Orders.GetOrderDetailsByOrderId
{
    public class GetOrderDetailsByOrderIdDataRequest : IRequest<GetOrderDetailsByOrderIdDataResponse>
    {
        public string OrderId { get; set; }
    }
}
