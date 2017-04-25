using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiLibrary.ApiModules.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ApiMethodAttribute : Attribute
    {
        public String ApiVersion { get; set; }
        public String ApiUri { get; set; }
        public String ApiMethod { get; set; }
        public String ApiNamespaceOverride { get; set; }
    }
}
