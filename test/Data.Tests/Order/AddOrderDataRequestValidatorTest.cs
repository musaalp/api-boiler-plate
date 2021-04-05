using Data.Orders.AddOrder;
using FluentValidation.TestHelper;
using Sdk.Core.Entities;
using System.Collections.Generic;
using Xunit;

namespace Data.Tests.Order
{
    public class AddOrderDataRequestValidatorTest
    {
        private readonly AddOrderDataRequestValidator _validator;

        public AddOrderDataRequestValidatorTest()
        {
            _validator = new AddOrderDataRequestValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("notemail")]
        [InlineData(null)]
        public void ValidateEmail_Invalid_ThrowsException(string email)
        {
            // Arrange            
            var request = new AddOrderDataRequest
            {
                Order = new OrderEntity
                {
                    Email = email
                }
            };

            // Assert
            _validator.ShouldHaveValidationErrorFor(r => r.Order.Email, request);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ValidateOrderId_Invalid_ThrowsException(string orderId)
        {
            // Arrange            
            var request = new AddOrderDataRequest
            {
                Order = new OrderEntity
                {
                    OrderId = orderId
                }
            };

            // Assert
            _validator.ShouldHaveValidationErrorFor(r => r.Order.OrderId, request);
        }

        [Fact]
        public void ValidateOrderDetails_Empty_ThrowsException()
        {
            // Arrange            
            var request = new AddOrderDataRequest
            {
                Order = new OrderEntity(),
                OrderDetails = new List<OrderDetailEntity>()
            };

            // Assert
            _validator.ShouldHaveValidationErrorFor(r => r.OrderDetails, request);
        }
    }
}
