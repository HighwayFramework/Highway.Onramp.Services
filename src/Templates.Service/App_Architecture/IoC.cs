// [[Highway.Onramp.Services]]
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Templates.App_Architecture
{
    public static class IoC
    {
        private readonly static object lockObject = new object();
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
