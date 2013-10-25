// [[Highway.Onramp.Services.Data]]
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Templates.App_Architecture.PlugIns.Data;
using Templates.App_Architecture.Services;
using Templates.Queries;

namespace Templates.App_Architecture.ServiceEvents
{
    public class EnsureDatabaseIsReachableStartServiceEvent : IStartServiceEvent
    {
        private readonly IRepositoryFactory repoFactory;

        public EnsureDatabaseIsReachableStartServiceEvent(IRepositoryFactory repoFactory)
        {
            this.repoFactory = repoFactory;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public int Order
        {
            get { return 10; }
        }

        public void Execute()
        {
            Logger.Info("Ensuring we can connect to a database");
            repoFactory.With(repo =>
            {
                var tableCount = repo.Find(new GetCountOfAllTables());
                Logger.InfoFormat("Discovered {0} tables in the database.", tableCount);
            });
        }
    }
}
