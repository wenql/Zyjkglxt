using System.Collections.Generic;
using TcmHMS.Authorization.Permissions.Dto;

namespace TcmHMS.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}