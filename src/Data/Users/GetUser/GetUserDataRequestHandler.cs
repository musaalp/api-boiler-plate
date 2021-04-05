using Dapper;
using Data.Utils;
using MediatR;
using Sdk.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Users.GetUser
{
    public class GetUserDataRequestHandler : IRequestHandler<GetUserDataRequest, UserEntity>
    {
        private readonly IOrderDbConnection _orderDbConnection;

        public GetUserDataRequestHandler(IOrderDbConnection orderDbConnection)
        {
            _orderDbConnection = orderDbConnection;
        }

        public async Task<UserEntity> Handle(GetUserDataRequest request, CancellationToken cancellationToken)
        {
            const string sql = @"
                                SELECT 
                                       u.Id,
                                       u.UserName,
                                       u.Email,
                                       u.FirstName,
                                       u.LastName
                                FROM Users u
                                WHERE u.UserName = @UserName
                                  AND u.PasswordHash = @PasswordHash
                                  AND u.IsDeleted = 0";

            using var connection = _orderDbConnection.GetConnection();

            return await connection.QueryFirstOrDefaultAsync<UserEntity>(sql, request);
        }
    }
}
