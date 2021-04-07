using MediatR;

namespace Data.Users.GetUserPermissions
{
    public class GetUserPermissionsDataRequest : IRequest<GetUserPermissionsDataResponse>
    {
        public int CompanyId { get; set; }
        public long UserId { get; set; }
    }
}
