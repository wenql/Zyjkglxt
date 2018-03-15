using Abp.Application.Navigation;
using Abp.Localization;
using TcmHMS.Authorization;

namespace TcmHMS.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class TcmHMSNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(new MenuItemDefinition(
                    "Dashboard",
                    L("工作台"),
                    url: "dashboard",
                    icon: "m-menu__link-icon flaticon-line-graph",
                    requiresAuthentication: true))
                .AddItem(new MenuItemDefinition(
                        "Administration",
                        L("系统管理"),
                        icon: "m-menu__link-icon flaticon-interface-8")
                    .AddItem(new MenuItemDefinition(
                        "Roles",
                        L("角色"),
                        url: "roles",
                        icon: "m-menu__link-icon flaticon-suitcase"))
                    .AddItem(new MenuItemDefinition(
                        "Users",
                        L("用户"),
                        url: "users",
                        icon: "m-menu__link-icon flaticon-users"))
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, TcmHMSConsts.LocalizationSourceName);
        }
    }
}
