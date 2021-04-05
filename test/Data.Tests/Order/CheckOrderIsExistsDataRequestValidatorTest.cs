using Data.Orders.CheckOrderIsExists;
using FluentValidation.TestHelper;
using Xunit;

namespace Data.Tests.Order
{
    public class CheckOrderIsExistsDataRequestValidatorTest
    {
        private readonly CheckOrderIsExistsDataRequestValidator _validator;

        public CheckOrderIsExistsDataRequestValidatorTest()
        {
            _validator = new CheckOrderIsExistsDataRequestValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ValidateOrderId_Invalid_ThrowsException(string orderId)
        {
            // Arrange            
            var request = new CheckOrderIsExistsDataRequest
            {
                OrderId = orderId
            };

            // Assert
            _validator.ShouldHaveValidationErrorFor(r => r.OrderId, request);
        }
    }
}
