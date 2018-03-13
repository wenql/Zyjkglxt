using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace TcmHMS.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : TcmHMSControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}