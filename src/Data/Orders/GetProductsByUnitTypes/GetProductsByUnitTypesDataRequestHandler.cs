using Dapper;
using Data.Utils;
using MediatR;
using Sdk.Core.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Orders.GetProductsByUnitTypes
{
    public class GetProductsByUnitTypesDataRequestHandler : IRequestHandler<GetProductsByUnitTypesDataRequest, GetProductsByUnitTypesDataResponse>
    {
        private readonly IOrderDbConnection _orderDbConnection;

        public GetProductsByUnitTypesDataRequestHandler(IOrderDbConnection orderDbConnection)
        {
            _orderDbConnection = orderDbConnection;
        }

        public async Task<GetProductsByUnitTypesDataResponse> Handle(GetProductsByUnitTypesDataRequest request, CancellationToken cancellationToken)
        {
            const string sql = @"
                                SELECT * 
                                FROM Products p
                                WHERE p.UnitType IN @UnitTypes;";

            using var connection = _orderDbConnection.GetConnection();
            connection.Open();

            var products = await connection.QueryAsync<ProductEntity>(sql, request);

            return new GetProductsByUnitTypesDataResponse
            {
                Products = products.ToList()
            };
        }
    }
}
