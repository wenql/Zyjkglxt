using System;
using Abp.Castle.Logging.Log4Net;
using Abp.Web;
using Castle.Facilities.Logging;
using Abp.WebApi.Validation;
using System.Threading;
using System.Globalization;

namespace TcmHMS.Web
{
    public class MvcApplication : AbpWebApplication<TcmHMSWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig(Server.MapPath("log4net.config"))
            );

            base.Application_Start(sender, e);

            //try
            //{
            //    //new AbpApiValidationFilter(AbpBootstrapper.IocManager, null).ExecuteActionFilterAsync(null, default(CancellationToken), null);

            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //    throw;
            //}
        }

        protected override void Application_BeginRequest(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
        }
    }
}
