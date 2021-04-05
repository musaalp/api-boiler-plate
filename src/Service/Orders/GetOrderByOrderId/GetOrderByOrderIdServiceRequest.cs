using MediatR;

namespace Service.Orders.GetOrderByOrderId
{
    public class GetOrderByOrderIdServiceRequest : IRequest<GetOrderByOrderIdServiceResponse>
    {
        public string OrderId { get; set; }
    }
}
