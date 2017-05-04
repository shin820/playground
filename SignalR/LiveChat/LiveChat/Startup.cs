using Microsoft.AspNet.SignalR;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var hubConfiguration = new HubConfiguration();
            //hubConfiguration.EnableDetailedErrors = true;
            //hubConfiguration.EnableJavaScriptProxies = false;
            //app.MapSignalR("/signalr", hubConfiguration);

            app.MapSignalR();
        }
    }
}