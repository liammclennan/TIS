using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace TodayIShall.Web.Infrastructure
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext context, string controllerName)
        {
            Type controllerType = base.GetControllerType(context, controllerName);
            if (controllerType == null) throw new Exception("Cant find controller: " + controllerName);
            return ObjectFactory.GetInstance(controllerType) as IController;
        }
    }
}
