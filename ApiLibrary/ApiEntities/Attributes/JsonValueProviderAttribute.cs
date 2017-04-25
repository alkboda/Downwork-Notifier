using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.ApiEntities.Attributes
{
    public class JsonValueProviderAttribute : Attribute
    {
        public JsonValueProviderAttribute() { }

        public Type ValueProviderType { get; set; }
    }
}
