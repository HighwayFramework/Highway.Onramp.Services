using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Castle.Components.DictionaryAdapter;

namespace Templates.App_Architecture.Configs
{
    [KeyPrefix("EntityFramework.")]
    public interface IConnectionStringConfig
    {
        string ConnectionString { get; set; }
    }
}