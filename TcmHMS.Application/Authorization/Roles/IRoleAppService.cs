using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TcmHMS.Authorization.Roles.Dto;

namespace TcmHMS.Authorization.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<RoleListDto>> GetRoles(GetRolesInput input);

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<PermissionDto>> GetAllPermissions();

        /// <summary>
        /// 获取待编辑实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdDto input);

        /// <summary>
        /// 保存角色变更
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateRole(CreateOrUpdateRoleInput input);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteRole(EntityDto input);
    }
}
