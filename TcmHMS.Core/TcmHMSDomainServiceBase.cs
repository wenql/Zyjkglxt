using Abp.Domain.Services;

namespace TcmHMS
{
    public abstract class TcmHMSDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected TcmHMSDomainServiceBase()
        {
            LocalizationSourceName = TcmHMSConsts.LocalizationSourceName;
        }
    }
}
