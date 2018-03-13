using System.Threading.Tasks;
using Abp.Application.Services;
using TcmHMS.Authorization.Accounts.Dto;

namespace TcmHMS.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RegisterOutput> Register(RegisterInput input);
    }
}
