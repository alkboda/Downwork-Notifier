using ApiLibrary.ApiEntities.Attributes;
using ApiLibrary.ApiEntities.Base;

namespace ApiLibrary.ApiModules.RequestParameters
{
    [JsonValueProvider(ValueProviderType = typeof(ToStringValueProvider))]
    public class RangeParameter<TValue> where TValue : struct
    {
        public TValue? Min { get; set; }
        public TValue? Max { get; set; }

        public override string ToString()
        {
            if (Min == null && Max == null)
            {
                return null;
            }

            return $"{Min}-{Max}";
        }
    }

    [JsonValueProvider(ValueProviderType = typeof(ToStringValueProvider))]
    public class Range<TClass> where TClass : class
    {
        public TClass Min { get; set; }
        public TClass Max { get; set; }

        public override string ToString()
        {
            if (Min == null && Max == null)
            {
                return null;
            }

            return $"{Min}-{Max}";
        }
    }
}
