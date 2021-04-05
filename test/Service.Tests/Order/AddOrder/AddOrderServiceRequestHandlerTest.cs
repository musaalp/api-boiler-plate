using Data.Orders.AddOrder;
using Data.Orders.CheckOrderIsExists;
using Data.Orders.GetProductsByUnitTypes;
using MediatR;
using Moq;
using Sdk.Core.Entities;
using Sdk.Core.Enums;
using Sdk.Core.Exceptions;
using Service.Orders.AddOrder;
using Service.Orders.Models;
using Service.Utils;
using Service.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Service.Tests.Order.AddOrder
{
    public class AddOrderServiceRequestHandlerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IBinWidthCalculator> _mockBinWidthCalculator;

        public AddOrderServiceRequestHandlerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockBinWidthCalculator = new Mock<IBinWidthCalculator>();
        }

        [Fact]
        public async Task Handle_OrderAlreadyExist_ThrowsException()
        {
            // Arrange           
            _mockMediator
                .Setup(m => m.Send(It.IsAny<CheckOrderIsExistsDataRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var request = new AddOrderServiceRequest();
            var handler = new AddOrderServiceRequestHandler(_mockMediator.Object, _mockBinWidthCalculator.Object);

            // Action          
            var exception = await Assert.ThrowsAsync<CustomException>(async () =>
                await handler.Handle(request, CancellationToken.None));

            // Assert
            Assert.True(exception.Message == TranslationKeys.Orders.OrderIsAlreadyExistWithGivenOrderId);
        }

        [Fact]
        public async Task Handle_ProductNotFoundWithGivenTypes_ThrowsException()
        {
            // Arrange           
            _mockMediator
                .Setup(m => m.Send(It.IsAny<CheckOrderIsExistsDataRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetProductsByUnitTypesDataRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetProductsByUnitTypesDataResponse
                {
                    Products = new List<ProductEntity>()
                });

            var request = new AddOrderServiceRequest
            {
                Products = new List<ProductModel>()
            };

            var handler = new AddOrderServiceRequestHandler(_mockMediator.Object, _mockBinWidthCalculator.Object);

            // Action          
            var exception = await Assert.ThrowsAsync<CustomException>(async () =>
                await handler.Handle(request, CancellationToken.None));

            // Assert
            Assert.True(exception.Message == TranslationKeys.Products.ProductIsNotFoundWithGivenUnitTypes);
        }

        [Fact]
        public async Task Handle_InternalServerError_ThrowsException()
        {
            // Arrange           
            decimal minBinWidth = 0;
            string unitType = ProductTypes.PhotoBook.ToString();

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CheckOrderIsExistsDataRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetProductsByUnitTypesDataRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetProductsByUnitTypesDataResponse
                {
                    Products = new List<ProductEntity>
                    {
                        new ProductEntity
                        {
                            Id = 1,
                            UnitSize = 19,
                            UnitType = unitType
                        }
                    }
                });

            _mockBinWidthCalculator
                .Setup(c => c.CalculateMinBinWidth(It.IsAny<List<ProductEntity>>()))
                .Returns(minBinWidth);

            _mockMediator
                .Setup(m => m.Send(It.IsAny<AddOrderDataRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var request = new AddOrderServiceRequest
            {
                Products = new List<ProductModel>
                {
                    new ProductModel
                    {
                        Quantity = 3,
                        UnitType = unitType
                    }
                }
            };

            var handler = new AddOrderServiceRequestHandler(_mockMediator.Object, _mockBinWidthCalculator.Object);

            // Action          
            var exception = await Assert.ThrowsAsync<SystemException>(async () =>
                await handler.Handle(request, CancellationToken.None));

            // Assert
            Assert.True(exception.Message == TranslationKeys.Db.InternalServerError);
        }

        [Fact]
        public async Task Handle_WithCorrectParams_ReturnsCorrectMinBinWidth()
        {
            // Arrange           
            decimal expectedMinBinWidth = 154.6M;
            string unitType = ProductTypes.Mug.ToString();

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CheckOrderIsExistsDataRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetProductsByUnitTypesDataRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetProductsByUnitTypesDataResponse
                {
                    Products = new List<ProductEntity>
                    {
                        new ProductEntity
                        {
                            Id = 5,
                            UnitSize = 94,
                            UnitType = unitType
                        }
                    }
                });

            _mockBinWidthCalculator
                .Setup(c => c.CalculateMinBinWidth(It.IsAny<List<ProductEntity>>()))
                .Returns(expectedMinBinWidth);

            _mockMediator
                .Setup(m => m.Send(It.IsAny<AddOrderDataRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var request = new AddOrderServiceRequest
            {
                Products = new List<ProductModel>
                {
                    new ProductModel
                    {
                        Quantity = 3,
                        UnitType = unitType
                    }
                }
            };

            var handler = new AddOrderServiceRequestHandler(_mockMediator.Object, _mockBinWidthCalculator.Object);

            // Action          
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(expectedMinBinWidth, result.MinBinWidth);
        }
    }
}
