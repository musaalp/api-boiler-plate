using FluentValidation.TestHelper;
using Service.Orders;
using Service.Orders.Models;
using Xunit;

namespace Service.Tests.Order
{
    public class ProductModelValidatorTest
    {
        private readonly ProductModelValidator _validator;

        public ProductModelValidatorTest()
        {
            _validator = new ProductModelValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ValidateUnitType_Invalid_ThrowsException(string unitType)
        {
            // Arrange            
            var model = new ProductModel
            {
                UnitType = unitType
            };

            // Assert
            _validator.ShouldHaveValidationErrorFor(r => r.UnitType, model);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void ValidateQuantity_Invalid_ThrowsException(int quantity)
        {
            // Arrange            
            var model = new ProductModel
            {
                Quantity = quantity
            };

            // Assert
            _validator.ShouldHaveValidationErrorFor(r => r.Quantity, model);
        }
    }
}
