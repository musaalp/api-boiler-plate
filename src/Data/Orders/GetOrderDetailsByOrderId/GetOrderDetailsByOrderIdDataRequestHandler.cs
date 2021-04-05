using Dapper;
using Data.Utils;
using MediatR;
using Sdk.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Orders.GetOrderDetailsByOrderId
{
    public class GetOrderDetailsByOrderIdDataRequestHandler : IRequestHandler<GetOrderDetailsByOrderIdDataRequest, GetOrderDetailsByOrderIdDataResponse>
    {
        private readonly IOrderDbConnection _orderDbConnection;

        public GetOrderDetailsByOrderIdDataRequestHandler(IOrderDbConnection orderDbConnection)
        {
            _orderDbConnection = orderDbConnection;
        }

        public async Task<GetOrderDetailsByOrderIdDataResponse> Handle(GetOrderDetailsByOrderIdDataRequest request, CancellationToken cancellationToken)
        {
            const string sql = @"
                                SELECT 
                                    O.OrderId,
                                    O.MinBinWidth,
	                                P.UnitType,
                                    OD.Quantity
                                FROM Orders O
	                                INNER JOIN OrderDetails OD ON O.Id = OD.OrderId
	                                INNER JOIN Products P ON OD.ProductId = P.Id
                                WHERE O.OrderId = @OrderId";

            using var connection = _orderDbConnection.GetConnection();
            connection.Open();

            var orderDetailDict = new Dictionary<string, GetOrderDetailsByOrderIdDataResponse>();

            var orderDetails = await connection.QueryAsync<GetOrderDetailsByOrderIdDataResponse, OrderDetailEntity,
                GetOrderDetailsByOrderIdDataResponse>(sql,
                (order, orderDetail) =>
                {
                    if (!orderDetailDict.TryGetValue(order.OrderId, out var orderEntry))
                    {
                        orderEntry = order;
                        orderEntry.OrderDetails = new List<OrderDetailEntity>();
                        orderDetailDict.Add(orderEntry.OrderId, orderEntry);
                    }

                    orderEntry.OrderDetails.Add(orderDetail);
                    return orderEntry;
                }, splitOn: "UnitType", param: request);

            return orderDetails.FirstOrDefault();
        }
    }
}
