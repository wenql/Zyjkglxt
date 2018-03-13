using System.Threading.Tasks;
using Abp.Application.Services;
using TcmHMS.Sessions.Dto;

namespace TcmHMS.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
