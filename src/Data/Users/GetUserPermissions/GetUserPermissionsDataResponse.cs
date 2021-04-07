using Data.Users.Models;
using System.Collections.Generic;

namespace Data.Users.GetUserPermissions
{
    public class GetUserPermissionsDataResponse
    {
        public List<PermissionModel> Permissions { get; set; }
    }
}
