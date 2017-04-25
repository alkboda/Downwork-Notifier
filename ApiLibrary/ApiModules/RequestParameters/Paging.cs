using ApiLibrary.ApiEntities.Attributes;
using ApiLibrary.ApiEntities.Base;
using Newtonsoft.Json;

namespace ApiLibrary.ApiModules.RequestParameters
{
    [JsonValueProvider(ValueProviderType = typeof(ToStringValueProvider))]
    public class Paging
    {
        public Paging(int offset = 0, int count = 100)
        {
            Offset = offset;
            Count = count;
        }

        [JsonProperty("offset")]
        public long Offset { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("total")]
        public long Total { get; set; }

        public override string ToString()
        {
            if (Offset == 0 && Count == 0)
            {
                return "0;100";
            }
            return $"{Offset};{Count}";
        }
    }
}
