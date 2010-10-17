using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TodayIShall.Web.Infrastructure
{
    public class RouteConfig
    {
        public void Initialize(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                "Today", // Route name
                "Today/{nameslug}", // URL with parameters
                new { controller = "Today", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "TodayAction", // Route name
                "Today/{nameslug}/{action}", // URL with parameters
                new { controller = "Today", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "About", // Route name
                "About", // URL with parameters
                new { controller = "About", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Registration", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
    }
}