using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using TcmHMS.Configuration.Dto;

namespace TcmHMS.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : TcmHMSAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
