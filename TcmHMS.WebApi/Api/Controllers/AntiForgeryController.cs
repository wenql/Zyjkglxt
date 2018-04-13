using Abp.Web.Security.AntiForgery;
using Abp.WebApi.Controllers;
using System.Net.Http;

namespace TcmHMS.Api.Controllers
{
    public class AntiForgeryController : AbpApiController
    {
        private readonly IAbpAntiForgeryManager _antiForgeryManager;

        public AntiForgeryController(IAbpAntiForgeryManager antiForgeryManager)
        {
            _antiForgeryManager = antiForgeryManager;
        }

        public HttpResponseMessage GetTokenCookie()
        {
            var response = new HttpResponseMessage();

            _antiForgeryManager.SetCookie(response.Headers);

            var i = _antiForgeryManager.GenerateToken();

            return response;
        }
    }
}
