using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Jax.Authorization;
using Jax.MultiTenancy;

namespace Jax.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantsController : JaxControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantsController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        public ActionResult Index()
        {
            var output = _tenantAppService.GetTenants();
            return View(output);
        }
    }
}