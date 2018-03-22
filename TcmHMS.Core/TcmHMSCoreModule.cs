using System.Reflection;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using TcmHMS.Authorization;
using TcmHMS.Authorization.Roles;
using TcmHMS.Authorization.Users;
using TcmHMS.Configuration;
using TcmHMS.MultiTenancy;

namespace TcmHMS
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class TcmHMSCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //Configuration.Localization.IsEnabled = false;

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    "TcmHMS",
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "TcmHMS.Localization.Source"
                        )
                    )
                );

            Configuration.Localization.Languages.Add(new LanguageInfo("zh-CN", "简体中文", isDefault: true));

            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Authorization.Providers.Add<TcmHMSAuthorizationProvider>();

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
