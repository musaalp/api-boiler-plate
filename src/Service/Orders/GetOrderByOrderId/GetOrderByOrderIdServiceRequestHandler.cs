using Data.Orders.GetOrderDetailsByOrderId;
using MediatR;
using Sdk.Core.Exceptions;
using Service.Orders.Models;
using Service.Utils;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Orders.GetOrderByOrderId
{
    public class GetOrderByOrderIdServiceRequestHandler : IRequestHandler<GetOrderByOrderIdServiceRequest, GetOrderByOrderIdServiceResponse>
    {
        private readonly IMediator _mediator;

        public GetOrderByOrderIdServiceRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<GetOrderByOrderIdServiceResponse> Handle(GetOrderByOrderIdServiceRequest request, CancellationToken cancellationToken)
        {
            var orderDetailsResponse = await _mediator.Send(new GetOrderDetailsByOrderIdDataRequest
            {
                OrderId = request.OrderId
            }, cancellationToken);

            if (orderDetailsResponse == null)
                throw new CustomException(TranslationKeys.Orders.OrderIsNotFoundWithGivenOrderId, HttpStatusCode.NotFound);

            return new GetOrderByOrderIdServiceResponse
            {
                OrderId = orderDetailsResponse.OrderId,
                MinBinWidth = orderDetailsResponse.MinBinWidth,
                OrderDetails = orderDetailsResponse.OrderDetails.Select(od => new OrderDetailModel
                {
                    UnitType = od.UnitType,
                    Quantity = od.Quantity

                }).ToList()
            };
        }
    }
}
