using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TcmHMS.Application.Authorization.Roles.Dto;

namespace TcmHMS.Application.Authorization.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<RoleListDto>> GetRoles(GetRolesInput input);

        Task<ListResultDto<PermissionDto>> GetAllPermissions();

        Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdDto input);
    }
}
