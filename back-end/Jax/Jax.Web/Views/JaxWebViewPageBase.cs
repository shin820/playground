using Abp.Web.Mvc.Views;

namespace Jax.Web.Views
{
    public abstract class JaxWebViewPageBase : JaxWebViewPageBase<dynamic>
    {

    }

    public abstract class JaxWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected JaxWebViewPageBase()
        {
            LocalizationSourceName = JaxConsts.LocalizationSourceName;
        }
    }
}