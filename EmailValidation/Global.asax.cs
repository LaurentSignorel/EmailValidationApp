﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]

namespace EmailValidation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }
    }
}
