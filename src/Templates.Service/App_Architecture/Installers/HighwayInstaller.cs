// [[Highway.Onramp.Services.Data]]
using Castle.MicroKernel.Registration;
using Common.Logging;
using Highway.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Facilities.TypedFactory;
using Templates.App_Architecture.PlugIns.Data;

namespace Templates.App_Architecture.Installers
{
    public class HighwayInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component.For<IDataContext>().ImplementedBy<DataContext>()
                    .DependsOn(Dependency.OnAppSettingsValue("connectionString", "Highway.ConnectionString"))
                    .LifeStyle.Transient,
                Component.For<IRepository>().ImplementedBy<Repository>()
                    .LifeStyle.Transient,
                Component.For<IMappingConfiguration>().ImplementedBy<HighwayMappings>(),
                Component.For<ILog>().UsingFactoryMethod((k, c) => LogManager.GetLogger("Highway")),
                Component.For<IContextConfiguration>().ImplementedBy<DefaultContextConfiguration>(),
                Component.For<IRepositoryFactory>().AsFactory().LifestyleTransient(),
                Component.For<IDatabaseInitializer<DataContext>>()
                    .ImplementedBy<DropCreateDatabaseIfModelChanges<DataContext>>()
                    .DependsOn(Dependency.OnAppSettingsValue("connectionString", "Highway.ConnectionString"))
                );

            Database.SetInitializer(container.Resolve<IDatabaseInitializer<DataContext>>());
        }
    }
}
