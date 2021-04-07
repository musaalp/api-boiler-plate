using Dapper;
using Data.Users.Models;
using Data.Utils;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Users.GetUserPermissions
{
    public class GetUserPermissionsDataRequestHandler : IRequestHandler<GetUserPermissionsDataRequest, GetUserPermissionsDataResponse>
    {
        private readonly IOrderDbConnection _orderDbConnection;

        public GetUserPermissionsDataRequestHandler(IOrderDbConnection orderDbConnection)
        {
            _orderDbConnection = orderDbConnection;
        }

        public async Task<GetUserPermissionsDataResponse> Handle(GetUserPermissionsDataRequest request,
            CancellationToken cancellationToken)
        {
            const string sql = @"SELECT DISTINCT p.Id, p.PermissionName AS Name, p.PermissionValue AS Value
                                FROM Users_Permissions p
                                         INNER JOIN Users_UserPermissions up ON p.Id = up.PermissionId OR p.IsPublic
                                WHERE up.UserId = @UserId;";

            using var connection = _orderDbConnection.GetConnection();

            var permissions = await connection.QueryAsync<PermissionModel>(sql, request);

            return new GetUserPermissionsDataResponse
            {
                Permissions = permissions.ToList()
            };
        }
    }
}
