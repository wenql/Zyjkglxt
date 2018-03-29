using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TcmHMS.Constitution.Dto;

namespace TcmHMS.Constitution
{
    public interface IConstitutionAppService : IApplicationService
    {
        Task<ListResultDto<ConstitutionGroupListDto>> GetConstitutionGroups();
    }
}
