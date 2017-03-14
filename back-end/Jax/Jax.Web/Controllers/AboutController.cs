using System.Web.Mvc;

namespace Jax.Web.Controllers
{
    public class AboutController : JaxControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}