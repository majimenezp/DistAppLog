using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.ViewEngines;
using Nancy.Bootstrapper;
namespace DistALServer.NancyKayak
{
    public class CustomBootStrapper:DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoC.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            //This should be the assembly your views are embedded in
            var assembly = GetType().Assembly;
            ResourceViewLocationProvider
                .RootNamespaces
                //TODO: replace NancyEmbeddedViews.MyViews with your resource prefix
                .Add(assembly, "DistALServer.Views");
        }
        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(OnConfigurationBuilder);
            }
        }

        void OnConfigurationBuilder(NancyInternalConfiguration x)
        {
            x.ViewLocationProvider = typeof(ResourceViewLocationProvider);
        }
    }
}
