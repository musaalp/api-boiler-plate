using Data.Orders.GetProductsByUnitTypes;
using FluentValidation.TestHelper;
using System.Collections.Generic;
using Xunit;

namespace Data.Tests.Order
{
    public class GetProductsByUnitTypeDataRequestValidatorTest
    {
        private readonly GetProductsByUnitTypesDataRequestValidator _validator;

        public GetProductsByUnitTypeDataRequestValidatorTest()
        {
            _validator = new GetProductsByUnitTypesDataRequestValidator();
        }

        [Fact]
        public void ValidateUnitTypes_Empty_ThrowsException()
        {
            // Arrange            
            var request = new GetProductsByUnitTypesDataRequest
            {
                UnitTypes = new List<string>()
            };

            // Assert
            _validator.ShouldHaveValidationErrorFor(r => r.UnitTypes, request);
        }
    }
}
