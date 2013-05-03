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
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Templates.Abstractions
{
    public static class IoC
    {
        private static object lockObject = new object();
        private static IWindsorContainer _Container;

        public static IWindsorContainer Container
        {
            get
            {
                if (_Container == null)
                    lock (lockObject)
                        if (_Container == null)
                            _Container = new WindsorContainer();

                return _Container;
            }
            set
            {
                if (_Container != null && value == null)
                    lock (lockObject)
                        if (_Container != null && value == null)
                            _Container = value;
            }
        }
    }
}
