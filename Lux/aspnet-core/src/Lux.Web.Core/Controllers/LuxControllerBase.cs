using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNet.Identity;

namespace Lux.Controllers
{
    public abstract class LuxControllerBase: AbpController
    {
        protected LuxControllerBase()
        {
            LocalizationSourceName = LuxConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}