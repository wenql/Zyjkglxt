using System.Threading.Tasks;
using Abp.Application.Services;
using TcmHMS.Configuration.Dto;

namespace TcmHMS.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}