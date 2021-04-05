using MediatR;

namespace Data.Orders.CheckOrderIsExists
{
    public class CheckOrderIsExistsDataRequest : IRequest<bool>
    {
        public string OrderId { get; set; }
    }
}
