using ApiLibrary.ApiEntities.Attributes;
using ApiLibrary.ApiEntities.Base;
using Newtonsoft.Json;

namespace ApiLibrary.ApiEntities.Auth
{
    public class Location : IEntity
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
