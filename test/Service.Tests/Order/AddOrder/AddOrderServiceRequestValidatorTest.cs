using FluentValidation.TestHelper;
using Service.Orders.AddOrder;
using Service.Orders.Models;
using System.Collections.Generic;
using Xunit;

namespace Service.Tests.Order.AddOrder
{
    public class AddOrderServiceRequestValidatorTest
    {
        private readonly AddOrderServiceRequestValidator _validator;

        public AddOrderServiceRequestValidatorTest()
        {
            _validator = new AddOrderServiceRequestValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("wrong-email-adress")]
        [InlineData(null)]
        public void ValidateEmail_Invalid_ThrowsException(string email)
        {
            // Arrange            
            var request = new AddOrderServiceRequest
            {
                Email = email
            };

            // Assert
            _validator.ShouldHaveValidationErrorFor(r => r.Email, request);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ValidateOrderId_Invalid_ThrowsException(string orderId)
        {
            // Arrange            
            var request = new AddOrderServiceRequest
            {
                OrderId = orderId
            };

            // Assert
            _validator.ShouldHaveValidationErrorFor(r => r.OrderId, request);
        }

        [Fact]
        public void ValidateProducts_Empty_ThrowsException()
        {
            // Arrange            
            var request = new AddOrderServiceRequest
            {
                Products = new List<ProductModel>()
            };

            // Assert
            _validator.ShouldHaveValidationErrorFor(r => r.Products, request);
        }
    }
}
