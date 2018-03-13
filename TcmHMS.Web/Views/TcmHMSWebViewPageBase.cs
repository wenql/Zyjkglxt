using Abp.Web.Mvc.Views;

namespace TcmHMS.Web.Views
{
    public abstract class TcmHMSWebViewPageBase : TcmHMSWebViewPageBase<dynamic>
    {

    }

    public abstract class TcmHMSWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected TcmHMSWebViewPageBase()
        {
            LocalizationSourceName = TcmHMSConsts.LocalizationSourceName;
        }
    }
}