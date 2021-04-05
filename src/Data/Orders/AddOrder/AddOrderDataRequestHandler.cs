using Dapper;
using Data.Utils;
using MediatR;
using Sdk.Helpers;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Orders.AddOrder
{
    public class AddOrderDataRequestHandler : IRequestHandler<AddOrderDataRequest, bool>
    {
        private readonly IOrderDbConnection _orderDbConnection;
        private readonly ISerializerHelper _serializerHelper;

        public AddOrderDataRequestHandler(IOrderDbConnection orderDbConnection, ISerializerHelper serializerHelper)
        {
            _orderDbConnection = orderDbConnection;
            _serializerHelper = serializerHelper;
        }

        public async Task<bool> Handle(AddOrderDataRequest request, CancellationToken cancellationToken)
        {
            const string orderSql = @"
                                    INSERT INTO Orders
                                    (
                                        OrderId,
                                        Email,
                                        MinBinWidth
                                    )
                                    VALUES
                                    (
                                        @OrderId,
                                        @Email,
                                        @MinBinWidth
                                    );

                                    SELECT LAST_INSERT_ID();";

            const string orderDetailSql = @"
                                          INSERT INTO OrderDetails
                                          (
                                              OrderId,
                                              ProductId,
                                              Quantity
                                          )
                                          SELECT 
                                                  @OrderId,
                                                  jt.ProductId,
                                                  jt.Quantity
                                          FROM JSON_TABLE(@OrderDetails, '$[*]' COLUMNS (                                        
                                              OrderId int PATH '$.OrderId',
                                              ProductId int PATH '$.ProductId',
                                              Quantity int PATH '$.Quantity')) AS jt;";

            using var connection = _orderDbConnection.GetConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                int orderId = await connection.ExecuteScalarAsync<int>(orderSql, request.Order, transaction);

                int affectedRows = await connection.ExecuteAsync(orderDetailSql, new
                {
                    OrderId = orderId,
                    OrderDetails = _serializerHelper.Serialize(request.OrderDetails)
                }, transaction);

                transaction.Commit();

                return affectedRows > 0;

            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
