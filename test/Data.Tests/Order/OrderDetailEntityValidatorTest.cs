using Data.Orders.AddOrder;
using FluentValidation.TestHelper;
using Sdk.Core.Entities;
using Xunit;

namespace Data.Tests.Order
{
    public class OrderDetailEntityValidatorTest
    {
        private readonly OrderDetailEntityValidator _validator;

        public OrderDetailEntityValidatorTest()
        {
            _validator = new OrderDetailEntityValidator();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void ValidateProductId_Invalid_ThrowsException(int productId)
        {
            // Arrange            
            var entity = new OrderDetailEntity
            {
                ProductId = productId
            };

            // Assert
            _validator.ShouldHaveValidationErrorFor(r => r.ProductId, entity);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void ValidateQuantity_Invalid_ThrowsException(int quantity)
        {
            // Arrange            
            var entity = new OrderDetailEntity
            {
                Quantity = quantity
            };

            // Assert
            _validator.ShouldHaveValidationErrorFor(r => r.Quantity, entity);
        }
    }
}
