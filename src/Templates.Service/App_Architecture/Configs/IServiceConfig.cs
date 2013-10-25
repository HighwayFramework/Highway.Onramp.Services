// [[Highway.Onramp.Services]]
using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templates.App_Architecture.Configs
{
    [KeyPrefix("Service.")]
    public interface IServiceConfig
    {
        string LongName { get; set; }
        string ShortName { get; set; }
        string Description { get; set; }
    }
}
