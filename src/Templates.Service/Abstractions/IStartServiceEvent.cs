using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Templates.Abstractions
{
    public interface IStartServiceEvent
    {
        int Order { get; }
        void Execute();
    }
}
