using System;

namespace ApiLibrary.ApiModules.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class UrlParameterAttribute : Attribute
    {
        public UrlParameterAttribute(String mapTo)
        {
            MapTo = mapTo;
        }

        public string MapTo { get; set; }
    }
}
