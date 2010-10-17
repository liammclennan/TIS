using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Norm;
using StructureMap;
using TodayIShall.Web.Infrastructure;

namespace TodayIShall.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            new RouteConfig().Initialize(RouteTable.Routes);
            new IoCConfig().Initialize();
            new MappingConfig().Initialize();
        }

        protected void Application_EndRequest()
        {
            ObjectFactory.GetInstance<IMongo>().Dispose();
        }
    }

}