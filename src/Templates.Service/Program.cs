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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Topshelf;
using Topshelf.ServiceConfigurators;
using Castle.Windsor.Installer;
using Templates.Abstractions;

namespace Templates
{
    class Program
    {
        static void Main(string[] args)
        {
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

                x.SetDescription(ConfigurationManager.AppSettings["Service.LongName"]);
                x.SetDisplayName(ConfigurationManager.AppSettings["Service.LongName"]);
                x.SetServiceName(ConfigurationManager.AppSettings["Service.ShortName"]);
            });
        }

        public class ContainerHostedService
        {
            private IHostedService hostedService;

            public void Start()
            {
                IoC.Container.Install(FromAssembly.This());

                hostedService = IoC.Container.Resolve<IHostedService>();

                hostedService.Start();
            }
            public void Stop()
            {
                if (hostedService != null)
                    hostedService.Stop();

                using (IoC.Container) { }
                IoC.Container = null;
            }
        }
    }
}