using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.ApiModules
{
    public interface IApiModule
    {
        String ApiNamespace { get; }
        Dictionary<String, String> MethodUris { get; }
    }
}
