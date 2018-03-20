using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TcmHMS.Authorization.Permissions.Dto;

namespace TcmHMS.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
