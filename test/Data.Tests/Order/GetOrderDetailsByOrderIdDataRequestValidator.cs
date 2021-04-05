using Data.Orders.GetOrderDetailsByOrderId;
using FluentValidation.TestHelper;
using Xunit;

namespace Data.Tests.Order
{
    public class GetOrderDetailsByOrderIdDataRequestValidatorTest
    {
        private readonly GetOrderDetailsByOrderIdDataRequestValidator _validator;

        public GetOrderDetailsByOrderIdDataRequestValidatorTest()
        {
            _validator = new GetOrderDetailsByOrderIdDataRequestValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ValidateOrderId_Invalid_ThrowsException(string orderId)
        {
            // Arrange            
            var request = new GetOrderDetailsByOrderIdDataRequest
            {
                OrderId = orderId
            };

            // Assert
            _validator.ShouldHaveValidationErrorFor(r => r.OrderId, request);
        }
    }
}
