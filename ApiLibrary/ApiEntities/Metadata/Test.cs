using ApiLibrary.ApiEntities.Attributes;
using ApiLibrary.ApiEntities.Base;
using Newtonsoft.Json;

namespace ApiLibrary.ApiEntities.Metadata
{
    [JsonPropertyMap(MappedJsonObjectName = "test", MappedJsonArrayName = "tests")]
    public class Test : IEntity
    {
        [JsonProperty("record_id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }
    }
}
