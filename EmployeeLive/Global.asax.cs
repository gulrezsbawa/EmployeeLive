using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//using Microsoft.AspNet.SignalR.Hosting;
//using Microsoft.AspNet.SignalR;
//using SignalR;
namespace EmployeeLive
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //RouteTable.Routes.MapHubs();
          //  RouteTable.Routes.MapHubs(new HubConfiguration { EnableCrossDomain = true });
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
