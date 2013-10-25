// [[Highway.Onramp.Services]]
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using Templates.App_Architecture.Services;

namespace Templates.App_Architecture.Installers
{
    public class ServiceEventsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().BasedOn<IStartServiceEvent>()
                .WithServiceFromInterface(typeof(IStartServiceEvent)),
                Classes.FromThisAssembly().BasedOn<IStopServiceEvent>()
                .WithServiceFromInterface(typeof(IStopServiceEvent))
            );
        }
    }
}
