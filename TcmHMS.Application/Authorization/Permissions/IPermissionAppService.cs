using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TcmHMS.Application.Authorization.Permissions.Dto;

namespace TcmHMS.Application.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
