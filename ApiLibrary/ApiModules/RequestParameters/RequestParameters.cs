using ApiLibrary.ApiModules.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ApiLibrary.ApiModules.RequestParameters
{
    public abstract class RequestParameters
    {
        public virtual Boolean IsDefault(PropertyInfo prop)
        {
            var propValue = prop.GetValue(this);
            return prop.PropertyType.GetTypeInfo().IsValueType && propValue.Equals(Activator.CreateInstance(prop.PropertyType));
        }

        public virtual Dictionary<String, String> ToUrlParameters(bool onlyUrlParameterAttributed = true, bool processNulls = false, bool processDefaults = false)
        {
            var result = new Dictionary<String, String>();
            foreach (var prop in GetType().GetTypeInfo().DeclaredProperties)
            {
                // If we decided to not process properties without UrlParameterAttribute then just skip em
                //
                var urlAttr = prop.GetCustomAttribute<UrlParameterAttribute>(true);
                if (onlyUrlParameterAttributed && urlAttr == null)
                {
                    continue;
                }

                // Taking property value for processing
                //
                var propValue = prop.GetValue(this);

                // Here we check for nulls if flag isn't set
                //
                if (!processNulls && (propValue == null || propValue.ToString() == null))
                {
                    continue;
                }

                // Here we check yet default values if appropriate flag isn't set
                //
                if (!processDefaults && IsDefault(prop))
                {
                    continue;
                }

                // Check on enum with redefined values for enum fields
                //
                if (prop.PropertyType.GetTypeInfo().IsEnum)
                {
                    var field = prop.PropertyType.GetTypeInfo().DeclaredFields.FirstOrDefault(_ => _.Name == propValue.ToString());
                    var fUrlAttr = field.GetCustomAttribute<UrlParameterAttribute>(true);
                    if (field != null && fUrlAttr != null)
                    {
                        propValue = fUrlAttr.MapTo ?? propValue;
                    }
                }

                // All checks are passed successfully, lets add pair to result dictionary
                //
                String propName = urlAttr?.MapTo ?? prop.Name;
                result.Add(OAuth.OAuthBase.UrlEncode(propName), OAuth.OAuthBase.UrlEncode(propValue?.ToString()));
                //result.Add(System.Net.WebUtility.UrlEncode(propName), System.Net.WebUtility.UrlEncode(propValue?.ToString()));
                //result.Add(propName, propValue?.ToString());
            }
            return result;
        }
    }
}
