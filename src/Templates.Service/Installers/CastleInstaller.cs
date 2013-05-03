// [[Highway.Onramp.Services]]
// Copyright 2013 Timothy J. Rayburn
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using Castle.Components.DictionaryAdapter;
using Castle.Facilities.Logging;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Templates.Service.Installers
{
    public class CastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Add the array resolver, so we can resolve Foo[] and IEnumerable<Foo>
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel, true));

            // Add all necessary facilities
            container.AddFacility<LoggingFacility>(l => l.UseNLog("NLog.config"));
            container.AddFacility<TypedFactoryFacility>();

            // Our configuration magic, register all interfaces ending in Config from
            // this assembly, and create implementations using DictionaryAdapter
            // from the AppSettings in our app.config.
            var daf = new DictionaryAdapterFactory();
            container.Register(
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
