using ApiLibrary.ApiEntities.Attributes;
using ApiLibrary.ApiEntities.Base;
using ApiLibrary.ApiEntities.Converters;
using Newtonsoft.Json;

namespace ApiLibrary.ApiEntities.Auth
{
    public class Capacity : IEntity
    {
        [JsonProperty("provider")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool IsProvider { get; set; }

        [JsonProperty("buyer")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool IsBuyer { get; set; }

        [JsonProperty("affiliate_manager")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool IsAffiliateManager { get; set; }
    }
}
