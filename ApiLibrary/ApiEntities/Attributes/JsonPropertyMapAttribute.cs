using System;

namespace ApiLibrary.ApiEntities.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class JsonPropertyMapAttribute : Attribute
    {
        public String MappedJsonObjectName { get; set; }
        public String MappedJsonArrayName { get; set; }
    }
}
