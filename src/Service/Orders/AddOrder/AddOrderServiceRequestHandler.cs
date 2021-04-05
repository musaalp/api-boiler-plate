using Data.Orders.AddOrder;
using Data.Orders.CheckOrderIsExists;
using Data.Orders.GetProductsByUnitTypes;
using MediatR;
using Sdk.Core.Entities;
using Sdk.Core.Exceptions;
using Service.Utils;
using Service.Utils.Helpers;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Orders.AddOrder
{
    public class AddOrderServiceRequestHandler : IRequestHandler<AddOrderServiceRequest, AddOrderServiceResponse>
    {
        private readonly IMediator _mediator;
        private readonly IBinWidthCalculator _binWidthCalculator;

        public AddOrderServiceRequestHandler(IMediator mediator, IBinWidthCalculator binWdithCalculator)
        {
            _mediator = mediator;
            _binWidthCalculator = binWdithCalculator;
        }

        public async Task<AddOrderServiceResponse> Handle(AddOrderServiceRequest request, CancellationToken cancellationToken)
        {
            bool isOrderExist = await _mediator.Send(new CheckOrderIsExistsDataRequest
            {
                OrderId = request.OrderId
            }, cancellationToken);

            if (isOrderExist)
                throw new CustomException(TranslationKeys.Orders.OrderIsAlreadyExistWithGivenOrderId, HttpStatusCode.Conflict);

            var productsDataResponse = await _mediator.Send(new GetProductsByUnitTypesDataRequest
            {
                UnitTypes = request.Products.Select(p => p.UnitType).ToList()
            }, cancellationToken);

            if (productsDataResponse.Products.Count < 1)
                throw new CustomException(TranslationKeys.Products.ProductIsNotFoundWithGivenUnitTypes, HttpStatusCode.NotFound);

            var products = request.Products
                .GroupBy(p => p.UnitType)
                .Select(p => new ProductEntity
                {
                    Id = productsDataResponse.Products.First(prd =>
                        string.Equals(p.Key, prd.UnitType, StringComparison.OrdinalIgnoreCase)).Id,
                    Quantity = p.Sum(s => s.Quantity),
                    UnitType = p.Key,
                    UnitSize = productsDataResponse.Products.First(prd =>
                        string.Equals(p.Key, prd.UnitType, StringComparison.OrdinalIgnoreCase)).UnitSize
                }).ToList();

            decimal minBinWidth = _binWidthCalculator.CalculateMinBinWidth(products);

            bool addedSuccessfuly = await _mediator.Send(new AddOrderDataRequest
            {
                Order = new OrderEntity
                {
                    OrderId = request.OrderId,
                    Email = request.Email,
                    MinBinWidth = minBinWidth,
                },
                OrderDetails = products.Select(p => new OrderDetailEntity
                {
                    ProductId = p.Id,
                    Quantity = p.Quantity

                }).ToList()
            });

            if (!addedSuccessfuly)
                throw new SystemException(TranslationKeys.Db.InternalServerError);

            return new AddOrderServiceResponse
            {
                MinBinWidth = minBinWidth
            };
        }
    }
}
