// [[Highway.Onramp.Services]]
using System;
using System.Linq;
using Castle.Windsor;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Highway.Data;

namespace Templates.App_Architecture.Installers
{
    public class DefaultConventionInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().Pick()
                    .WithServiceDefaultInterfaces()
                    .LifestyleSingleton()
                );
        }
    }
}
