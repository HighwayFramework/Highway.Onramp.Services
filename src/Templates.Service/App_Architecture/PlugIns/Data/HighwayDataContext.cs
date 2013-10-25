using Highway.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Templates.App_Architecture.Configs;

namespace Templates.App_Architecture.PlugIns.Data
{
    public class HighwayDataContext : DataContext
    {
        public HighwayDataContext(IConnectionStringConfig config, IMappingConfiguration mapping, IContextConfiguration contextConfiguration, ILog log)
            : base(config.ConnectionString, mapping, contextConfiguration, log)
        {
        }
    }
}