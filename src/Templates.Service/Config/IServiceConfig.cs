// [[Highway.Onramp.Services]]
// This is an example of how to access App.Config settings
using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templates.Service.Config
{
    [KeyPrefix("Service.")]
    public interface IServiceConfig
    {
        string LongName { get; set; }
        string ShortName { get; set; }
    }
}
