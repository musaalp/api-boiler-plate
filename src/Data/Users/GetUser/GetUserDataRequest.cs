using MediatR;
using Sdk.Core.Entities;

namespace Data.Users.GetUser
{
    public class GetUserDataRequest : IRequest<UserEntity>
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
