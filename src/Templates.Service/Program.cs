// [[Highway.Onramp.Services]]
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Topshelf;
using Topshelf.ServiceConfigurators;
using Castle.Windsor.Installer;
using Templates.App_Architecture;
using Templates.App_Architecture.Configs;
using Templates.App_Architecture.Activators;
using Templates.App_Architecture.Services;

namespace Templates
{
    class Program
    {
        static void Main(string[] args)
        {
            WindsorActivator.Startup();
            HostFactory.Run(x =>
            {
                Action<ServiceConfigurator<ContainerHostedService>> config = s =>
                {
                    s.WhenStarted(tc =>
                    {
                        tc.Start();
                    });
                    s.WhenStopped(tc =>
                    {
                        tc.Stop();
                    });
                    s.ConstructUsing(name =>
                    {
                        return new ContainerHostedService();
                    });
                };

                x.Service<ContainerHostedService>(config);

                x.RunAsLocalSystem();
                x.UseNLog();

                var svcNameConfig = IoC.Container.Resolve<IServiceConfig>();

                x.SetDescription(svcNameConfig.Description);
                x.SetDisplayName(svcNameConfig.LongName);
                x.SetServiceName(svcNameConfig.ShortName);
            });
        }

        public class ContainerHostedService
        {
            private IHostedService hostedService;
            private IStartServiceEvent[] startEvents = new IStartServiceEvent[] {};
            private IStopServiceEvent[] stopEvents = new IStopServiceEvent[] {};

            public void Start()
            {
                IoC.Container.Install(FromAssembly.This());

                hostedService = IoC.Container.Resolve<IHostedService>();
                startEvents = IoC.Container.ResolveAll<IStartServiceEvent>();
                stopEvents = IoC.Container.ResolveAll<IStopServiceEvent>();

                foreach (var startServiceEvent in startEvents.OrderBy(e => e.Order))
                {
                    startServiceEvent.Execute();
                }

                hostedService.Start();
            }
            public void Stop()
            {
                if (hostedService != null)
                    hostedService.Stop();

                foreach (var stopServiceEvent in stopEvents.OrderBy(e => e.Order))
                {
                    stopServiceEvent.Execute();
                }

                using (IoC.Container) { }
                IoC.Container = null;
            }
        }
    }
}