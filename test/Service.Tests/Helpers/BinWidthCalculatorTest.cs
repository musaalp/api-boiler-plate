using Sdk.Core.Entities;
using Sdk.Core.Enums;
using Service.Utils.Helpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace Service.Tests.Helpers
{
    public class BinWidthCalculatorTest
    {
        [Fact]
        public void CalculateMinBinWidth_WithNullProducts_ThrowsException()
        {
            // Arrange                        
            var calculator = new BinWdithCalculator();

            // Assert
            Assert.Throws<ArgumentNullException>(() => calculator.CalculateMinBinWidth(null));
        }

        [Fact]
        public void CalculateMinBinWidth_WithEmptyProducts_ReturnsZero()
        {
            // Arrange            
            var products = new List<ProductEntity>();
            var calculator = new BinWdithCalculator();

            //Action
            decimal expected = 0;
            decimal actual = calculator.CalculateMinBinWidth(products);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateMinBinWidth_WithoutMugProducts_ReturnsCorrectWidth()
        {
            // Arrange            
            var products = new List<ProductEntity>
            {
                new ProductEntity
                {
                    Quantity = 5,
                    UnitType = ProductTypes.PhotoBook.ToString(),
                    UnitSize = 19
                },
                new ProductEntity
                {
                    Quantity = 3,
                    UnitType = ProductTypes.Calendar.ToString(),
                    UnitSize = 10
                },
                new ProductEntity
                {
                    Quantity = 2,
                    UnitType = ProductTypes.Canvas.ToString(),
                    UnitSize = 16
                },
                new ProductEntity
                {
                    Quantity = 6,
                    UnitType = ProductTypes.Cards.ToString(),
                    UnitSize = 4.7M
                },
            };
            var calculator = new BinWdithCalculator();

            //Action
            decimal expected = 185.2M;
            decimal actual = calculator.CalculateMinBinWidth(products);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateMinBinWidth_WithOnlyMugProducts_ReturnsCorrectWidth()
        {
            // Arrange            
            var products = new List<ProductEntity>
            {
                new ProductEntity
                {
                    Quantity = 15,
                    UnitType = ProductTypes.Mug.ToString(),
                    UnitSize = 94
                }
            };
            var calculator = new BinWdithCalculator();

            //Action
            decimal expected = 376;
            decimal actual = calculator.CalculateMinBinWidth(products);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateMinBinWidth_WithAllProducts_ReturnsCorrectWidth()
        {
            // Arrange            
            var products = new List<ProductEntity>
            {
                new ProductEntity
                {
                    Quantity = 5,
                    UnitType = ProductTypes.PhotoBook.ToString(),
                    UnitSize = 19
                },
                new ProductEntity
                {
                    Quantity = 3,
                    UnitType = ProductTypes.Calendar.ToString(),
                    UnitSize = 10
                },
                new ProductEntity
                {
                    Quantity = 2,
                    UnitType = ProductTypes.Canvas.ToString(),
                    UnitSize = 16
                },
                new ProductEntity
                {
                    Quantity = 6,
                    UnitType = ProductTypes.Cards.ToString(),
                    UnitSize = 4.7M
                },
                new ProductEntity
                {
                    Quantity = 9,
                    UnitType = ProductTypes.Mug.ToString(),
                    UnitSize = 94
                },
            };
            var calculator = new BinWdithCalculator();

            //Action
            decimal expected = 467.2M;
            decimal actual = calculator.CalculateMinBinWidth(products);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
