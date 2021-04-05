using Dapper;
using Data.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Orders.CheckOrderIsExists
{
    public class CheckOrderIsExistsDataRequestHandler : IRequestHandler<CheckOrderIsExistsDataRequest, bool>
    {
        private readonly IOrderDbConnection _orderDbConnection;

        public CheckOrderIsExistsDataRequestHandler(IOrderDbConnection orderDbConnection)
        {
            _orderDbConnection = orderDbConnection;
        }

        public async Task<bool> Handle(CheckOrderIsExistsDataRequest request, CancellationToken cancellationToken)
        {
            const string sql = @"
                                SELECT IF(Count(O.Id) > 0, 1, 0) 
                                FROM Orders O
                                WHERE O.OrderId = @OrderId;";

            using var connection = _orderDbConnection.GetConnection();
            connection.Open();

            return await connection.QueryFirstAsync<bool>(sql, request);
        }
    }
}
