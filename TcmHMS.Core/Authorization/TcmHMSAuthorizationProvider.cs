using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace TcmHMS.Authorization
{
    public class TcmHMSAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull(PermissionNames.Pages) ?? context.CreatePermission(PermissionNames.Pages, L("页面"));

            var administration = pages.CreateChildPermission(PermissionNames.Pages_Administration, L("系统管理"));

            var roles = administration.CreateChildPermission(PermissionNames.Pages_Administration_Roles, L("角色"));
            roles.CreateChildPermission(PermissionNames.Pages_Administration_Roles_Create, L("添加角色"));
            roles.CreateChildPermission(PermissionNames.Pages_Administration_Roles_Edit, L("修改角色"));
            roles.CreateChildPermission(PermissionNames.Pages_Administration_Roles_Delete, L("删除角色"));

            var users = administration.CreateChildPermission(PermissionNames.Pages_Administration_Users, L("用户"));
            users.CreateChildPermission(PermissionNames.Pages_Administration_Users_Create, L("添加用户"));
            users.CreateChildPermission(PermissionNames.Pages_Administration_Users_Edit, L("修改用户"));
            users.CreateChildPermission(PermissionNames.Pages_Administration_Users_Delete, L("删除用户"));
            users.CreateChildPermission(PermissionNames.Pages_Administration_Users_ChangePermissions, L("更改权限"));
            users.CreateChildPermission(PermissionNames.Pages_Administration_Users_Impersonation, L("切换至此用户"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, TcmHMSConsts.LocalizationSourceName);
        }
    }
}
