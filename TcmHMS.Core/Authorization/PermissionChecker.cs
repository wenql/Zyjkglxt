using Abp.Authorization;
using TcmHMS.Authorization.Roles;
using TcmHMS.Authorization.Users;

namespace TcmHMS.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
