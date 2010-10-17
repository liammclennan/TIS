using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Norm;
using StructureMap;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;
using TodayIShall.Core.AppServices;
using TodayIShall.Web.Controllers;

namespace TodayIShall.Web.Infrastructure
{
    public class IoCConfig
    {
        public void Initialize()
        {
            ObjectFactory.Initialize(ConfigureStructureMap);
            ControllerBuilder.Current.SetControllerFactory(typeof(StructureMapControllerFactory));
        }

        private void ConfigureStructureMap(IInitializationExpression initializer)
        {
            initializer.Scan(y =>
            {
                y.Assembly("TodayIShall.Core");
                y.WithDefaultConventions();
            });
            initializer.ForRequestedType<IAuthCookieGenerator>().TheDefaultIsConcreteType<AuthCookieGenerator>();
            initializer.AddRegistry(new MongoRegistry());
        }

    }

    public class MongoRegistry : Registry
    {
        public MongoRegistry()
        {
            ForRequestedType<IMongo>()
                .CacheBy(InstanceScope.Hybrid)
                .TheDefault.Is.ConstructedBy(factory => Mongo.Create(factory.GetInstance<IConfigService>().MongoConnectionString))
                ;
        }
    }
}