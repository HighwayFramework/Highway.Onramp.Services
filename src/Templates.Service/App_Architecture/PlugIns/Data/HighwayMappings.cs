// [[Highway.Onramp.Services.Data]]
using Highway.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Templates.Entities;

namespace Templates.App_Architecture.PlugIns.Data
{
    public class HighwayMappings : IMappingConfiguration
    {
        public void ConfigureModelBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExampleEntity>();
        }
    }
}
