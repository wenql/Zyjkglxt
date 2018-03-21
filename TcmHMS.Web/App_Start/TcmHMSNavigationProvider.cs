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
                    L("Dashboard"),
                    url: "dashboard",
                    icon: "m-menu__link-icon flaticon-line-graph",
                    requiresAuthentication: true)
                ).AddItem(new MenuItemDefinition(
                        "Administration",
                        L("Administration"),
                        icon: "m-menu__link-icon flaticon-interface-8")
                    .AddItem(new MenuItemDefinition(
                        "Roles",
                        L("Administration.Roles"),
                        url: "roles",
                        icon: "m-menu__link-icon flaticon-suitcase",
                        requiredPermissionName: PermissionNames.Pages_Administration_Roles)
                    ).AddItem(new MenuItemDefinition(
                        "Users",
                        L("Administration.Users"),
                        url: "users",
                        icon: "m-menu__link-icon flaticon-users",
                        requiredPermissionName: PermissionNames.Pages_Administration_Users)
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, TcmHMSConsts.LocalizationSourceName);
        }
    }
}
