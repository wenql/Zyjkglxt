using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TcmHMS.MultiTenancy.Dto;

namespace TcmHMS.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
