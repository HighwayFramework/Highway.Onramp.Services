// [[Highway.Onramp.Services.Data]]
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
using Castle.MicroKernel.Registration;
using Common.Logging;
using Highway.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templates.Abstractions;
using Templates.Service.Config;
using Castle.Facilities.TypedFactory;

namespace Templates.Service.Installers
{
    public class HighwayInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component.For<IDataContext>().ImplementedBy<DataContext>()
                    .DependsOn(Dependency.OnAppSettingsValue("connectionString", "HighwayData.ConnectionString"))
                    .LifeStyle.Transient,
                Component.For<IRepository>().ImplementedBy<Repository>()
                    .LifeStyle.Transient,
                Component.For<IMappingConfiguration>().ImplementedBy<HighwayMappings>(),
                Component.For<ILog>().UsingFactoryMethod((k, c) => LogManager.GetLogger("Highway")),
                Component.For<IContextConfiguration>().ImplementedBy<HighwayContextConfiguration>(),
                Component.For<IRepositoryFactory>().AsFactory(),
                Component.For<IDatabaseInitializer<DataContext>>()
                    .ImplementedBy<DropCreateDatabaseIfModelChanges<DataContext>>()
                    .DependsOn(Dependency.OnAppSettingsValue("connectionString", "HighwayDataConnectionString"))
                );

            Database.SetInitializer(container.Resolve<IDatabaseInitializer<DataContext>>());
        }
    }
}
