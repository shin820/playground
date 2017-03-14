using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace Jax.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : JaxControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}