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
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using Templates.Abstractions;

namespace Templates.Service.Installers
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
