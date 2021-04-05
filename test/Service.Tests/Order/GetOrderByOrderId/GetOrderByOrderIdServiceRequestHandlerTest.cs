using Data.Orders.GetOrderDetailsByOrderId;
using MediatR;
using Moq;
using Sdk.Core.Entities;
using Sdk.Core.Exceptions;
using Service.Orders.GetOrderByOrderId;
using Service.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Service.Tests.Order.GetOrderByOrderId
{
    public class GetOrderByOrderIdServiceRequestHandlerTest
    {
        private readonly Mock<IMediator> _mockMediator;

        public GetOrderByOrderIdServiceRequestHandlerTest()
        {
            _mockMediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task Handle_OrderIsNotFound_ThrowsException()
        {
            // Arrange           
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetOrderDetailsByOrderIdDataRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);

            var request = new GetOrderByOrderIdServiceRequest();
            var handler = new GetOrderByOrderIdServiceRequestHandler(_mockMediator.Object);

            // Action          
            var exception = await Assert.ThrowsAsync<CustomException>(async () =>
                await handler.Handle(request, CancellationToken.None));

            // Assert
            Assert.True(exception.Message == TranslationKeys.Orders.OrderIsNotFoundWithGivenOrderId);
        }

        [Fact]
        public async Task Handle_WithValidOrderId_ReturnsOrder()
        {
            // Arrange 
            string orderId = Guid.NewGuid().ToString();
            decimal minBinWidth = 134.7M;

            var orderResponse = new GetOrderDetailsByOrderIdDataResponse
            {
                OrderId = orderId,
                MinBinWidth = minBinWidth,
                OrderDetails = new List<OrderDetailEntity>()
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetOrderDetailsByOrderIdDataRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(orderResponse);

            var request = new GetOrderByOrderIdServiceRequest();
            var handler = new GetOrderByOrderIdServiceRequestHandler(_mockMediator.Object);

            // Action          
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsAssignableFrom<GetOrderByOrderIdServiceResponse>(result);
            Assert.Equal(minBinWidth, result.MinBinWidth);
        }
    }
}
