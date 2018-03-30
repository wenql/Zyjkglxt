using Abp.Authorization;
using Abp.Localization;

namespace TcmHMS.Authorization
{
    public class TcmHMSAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull(PermissionNames.Pages) ?? context.CreatePermission(PermissionNames.Pages, L("页面"));

            var constitution = pages.CreateChildPermission(PermissionNames.Pages_Constitutions, L("体质辨识"));

            constitution.CreateChildPermission(PermissionNames.Pages_Constitutions_Groups, L("体质分类"));

            var subjects = constitution.CreateChildPermission(PermissionNames.Pages_Constitutions_Subjects, L("体质问卷"));
            subjects.CreateChildPermission(PermissionNames.Pages_Constitutions_Subjects_Create, L("添加问卷"));
            subjects.CreateChildPermission(PermissionNames.Pages_Constitutions_Subjects_Edit, L("修改问卷"));
            subjects.CreateChildPermission(PermissionNames.Pages_Constitutions_Subjects_Delete, L("删除问卷"));

            var suggests = constitution.CreateChildPermission(PermissionNames.Pages_Constitutions_Suggests, L("健康建议"));
            suggests.CreateChildPermission(PermissionNames.Pages_Constitutions_Suggests_Create, L("添加建议"));
            suggests.CreateChildPermission(PermissionNames.Pages_Constitutions_Suggests_Edit, L("修改建议"));
            suggests.CreateChildPermission(PermissionNames.Pages_Constitutions_Suggests_Delete, L("删除建议"));


            var dictionaries = pages.CreateChildPermission(PermissionNames.Pages_Dictionaries, L("数据字典"));

            var departments = dictionaries.CreateChildPermission(PermissionNames.Pages_Dictionaries_Departments, L("科室"));
            departments.CreateChildPermission(PermissionNames.Pages_Dictionaries_Departments_Create, L("添加科室"));
            departments.CreateChildPermission(PermissionNames.Pages_Dictionaries_Departments_Edit, L("修改科室"));
            departments.CreateChildPermission(PermissionNames.Pages_Dictionaries_Departments_Delete, L("删除科室"));

            var diseases = dictionaries.CreateChildPermission(PermissionNames.Pages_Dictionaries_Diseases, L("病症"));
            diseases.CreateChildPermission(PermissionNames.Pages_Dictionaries_Diseases_Create, L("添加病症"));
            diseases.CreateChildPermission(PermissionNames.Pages_Dictionaries_Diseases_Edit, L("修改病症"));
            diseases.CreateChildPermission(PermissionNames.Pages_Dictionaries_Diseases_Delete, L("删除病症"));

            var ranks = dictionaries.CreateChildPermission(PermissionNames.Pages_Dictionaries_Ranks, L("职称"));
            ranks.CreateChildPermission(PermissionNames.Pages_Dictionaries_Ranks_Create, L("添加职称"));
            ranks.CreateChildPermission(PermissionNames.Pages_Dictionaries_Ranks_Edit, L("修改职称"));
            ranks.CreateChildPermission(PermissionNames.Pages_Dictionaries_Ranks_Delete, L("删除职称"));


            var medicines = dictionaries.CreateChildPermission(PermissionNames.Pages_Dictionaries_Medicines, L("药品"));
            medicines.CreateChildPermission(PermissionNames.Pages_Dictionaries_Medicines_Create, L("添加药品"));
            medicines.CreateChildPermission(PermissionNames.Pages_Dictionaries_Medicines_Edit, L("修改药品"));
            medicines.CreateChildPermission(PermissionNames.Pages_Dictionaries_Medicines_Delete, L("删除药品"));

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
