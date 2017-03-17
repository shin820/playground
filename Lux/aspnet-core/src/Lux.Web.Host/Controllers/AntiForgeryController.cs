using Lux.Controllers;
using Microsoft.AspNetCore.Antiforgery;

namespace Lux.Web.Host.Controllers
{
    public class AntiForgeryController : LuxControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}