// [[Highway.Onramp.Services.Data]]
using Castle.Core.Logging;
using Highway.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Templates.App_Architecture.PlugIns.Data
{
    public interface IRepositoryFactory
    {
        IRepository CreateRepository();
        void ReleaseRepository(IRepository repo);
    }

    public static class RepositoryFactoryExtensions
    {
        public static void With(this IRepositoryFactory factory, Action<IRepository> action)
        {
            var repo = factory.CreateRepository();
            try
            {
                action.Invoke(repo);
            }
            finally
            {
                factory.ReleaseRepository(repo);
            }
        }
    }
}
