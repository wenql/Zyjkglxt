﻿using Abp.Authorization;
using Abp.Localization;
using Abp.Notifications;
using TcmHMS.Authorization;
using TcmHMS.Notifications;

namespace TcmHMS.Notifications
{
    public class AppNotificationProvider : NotificationProvider
    {
        public override void SetNotifications(INotificationDefinitionContext context)
        {
            context.Manager.Add(
                new NotificationDefinition(
                    AppNotificationNames.NewUserRegistered,
                    displayName: L("NewUserRegisteredNotificationDefinition"),
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Administration_Users)
                    )
                );

            //context.Manager.Add(
            //    new NotificationDefinition(
            //        AppNotificationNames.NewTenantRegistered,
            //        displayName: L("NewTenantRegisteredNotificationDefinition"),
            //        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Tenants)
            //        )
            //    );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, "");
        }
    }
}
