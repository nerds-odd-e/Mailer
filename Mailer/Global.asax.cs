using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using dotenv.net;

namespace Mailer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var rootPath = AppDomain.CurrentDomain.BaseDirectory;
            AutofacConfig.RegisterConfig();
            DotEnv.Config(true, rootPath + "/.env");
        }
    }
}
