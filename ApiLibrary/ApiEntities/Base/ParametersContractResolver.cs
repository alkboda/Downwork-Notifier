using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ApiLibrary.ApiEntities.Attributes;
using System.Reflection;

namespace ApiLibrary.ApiEntities.Base
{
    public class ParametersContractResolver : DefaultContractResolver
    {
        public ParametersContractResolver() { }

        protected override IValueProvider CreateMemberValueProvider(MemberInfo member)
        {
            var defaultProvider = base.CreateMemberValueProvider(member);
            
            var providerType = (member as PropertyInfo)?.PropertyType.GetTypeInfo().GetCustomAttributes<JsonValueProviderAttribute>(false).FirstOrDefault()?.ValueProviderType;
            if (providerType != null)
            {
                return (IValueProvider)Activator.CreateInstance(providerType, member, defaultProvider);
            }

            return defaultProvider;
        }
    }
}
