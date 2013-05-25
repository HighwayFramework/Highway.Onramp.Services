using Castle.Core.Logging;
using Highway.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Templates.Abstractions
{
    public interface IRepositoryFactory
    {
        IRepository CreateRepository();
        void ReleaseRepository();
    }
}
