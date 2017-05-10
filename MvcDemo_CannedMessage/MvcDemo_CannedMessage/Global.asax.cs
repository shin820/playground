using Castle.Windsor;
using log4net;
using MvcDemo_CannedMessage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcDemo_CannedMessage
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start()
        {
            log.Info("Application starting...");
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IocContainer.Setup();
            log.Info("Application started...");
        }

        protected void Application_Error()
        {
            var raisedException = Server.GetLastError();
            if (raisedException != null)
            {
                log.Error(raisedException.Message, raisedException);
            }

            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                RequestContext requestContext = ((MvcHandler)httpContext.CurrentHandler).RequestContext;

                if (requestContext.HttpContext.Request.IsAjaxRequest())
                {
                    httpContext.Response.Clear();
                    string controllerName = requestContext.RouteData.GetRequiredString("controller");
                    IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
                    IController controller = factory.CreateController(requestContext, controllerName);
                    ControllerContext controllerContext = new ControllerContext(requestContext, (ControllerBase)controller);

                    JsonResult jsonResult = new JsonResult();
                    jsonResult.Data = new { success = false, serverError = "500" };
                    jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    jsonResult.ExecuteResult(controllerContext);
                    httpContext.Response.End();
                }
                //else
                //{
                //    httpContext.Response.Redirect("~/Error");
                //}
            }
        }
    }
}
