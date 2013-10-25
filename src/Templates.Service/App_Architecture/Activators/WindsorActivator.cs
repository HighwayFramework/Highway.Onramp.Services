using Castle.Components.DictionaryAdapter;
using Castle.Facilities.Logging;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templates.App_Architecture;

namespace Templates.App_Architecture.Activators
{
    public static class WindsorActivator
    {
        public static void Startup()
        {
            // Add the array resolver, so we can resolve Foo[] and IEnumerable<Foo>
            IoC.Container.Kernel.Resolver.AddSubResolver(new ArrayResolver(IoC.Container.Kernel, true));

            // Add all necessary facilities
            IoC.Container.AddFacility<LoggingFacility>(l => l.UseNLog("NLog.config"));
            IoC.Container.AddFacility<TypedFactoryFacility>();

            // Our configuration magic, register all interfaces ending in Config from
            // this assembly, and create implementations using DictionaryAdapter
            // from the AppSettings in our app.config.
            var daf = new DictionaryAdapterFactory();
            IoC.Container.Register(
                Types
                    .FromThisAssembly()
                    .Where(type => type.IsInterface && type.Name.EndsWith("Config"))
                    .Configure(
                        reg => reg.UsingFactoryMethod(
                            (k, m, c) => daf.GetAdapter(m.Implementation, ConfigurationManager.AppSettings)
                            )
                    ));
        }
    }
}
