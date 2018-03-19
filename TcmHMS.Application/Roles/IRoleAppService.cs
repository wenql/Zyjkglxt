using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TcmHMS.Roles.Dto;

namespace TcmHMS.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<RoleListDto>> GetRoles(GetRolesInput input);

        Task<ListResultDto<PermissionDto>> GetAllPermissions();
    }
}
