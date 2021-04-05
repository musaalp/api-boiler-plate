using Api.Models.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sdk.Api.Utils;
using Service.Orders.AddOrder;
using Service.Orders.GetOrderByOrderId;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get order by given order Id.
        /// </summary>
        [HttpGet("{orderId}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrderById([FromRoute] string orderId, CancellationToken cancellationToken)
        {
            var orderReponse = await _mediator.Send(new GetOrderByOrderIdServiceRequest
            {
                OrderId = orderId
            }, cancellationToken);

            return ApiActionResult.Ok(orderReponse);
        }

        /// <summary>
        ///     Add order data.
        /// </summary>
        [HttpPost]

        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderModel model, CancellationToken cancellationToken)
        {
            var addOrderResponse = await _mediator.Send(new AddOrderServiceRequest
            {
                OrderId = model.OrderId,
                Email = model.Email,
                Products = model.Products

            }, cancellationToken);

            return ApiActionResult.Created(addOrderResponse);
        }
    }
}