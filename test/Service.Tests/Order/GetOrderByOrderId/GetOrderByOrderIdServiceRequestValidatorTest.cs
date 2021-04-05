using FluentValidation.TestHelper;
using Service.Orders.GetOrderByOrderId;
using Xunit;

namespace Service.Tests.Order.GetOrderByOrderId
{
    public class GetOrderByOrderIdServiceRequestValidatorTest
    {
        private readonly GetOrderByOrderIdServiceRequestValidator _validator;

        public GetOrderByOrderIdServiceRequestValidatorTest()
        {
            _validator = new GetOrderByOrderIdServiceRequestValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ValidateOrderId_Invalid_ThrowsException(string orderId)
        {
            // Arrange            
            var request = new GetOrderByOrderIdServiceRequest
            {
                OrderId = orderId
            };

            // Assert
            _validator.ShouldHaveValidationErrorFor(r => r.OrderId, request);
        }
    }
}
