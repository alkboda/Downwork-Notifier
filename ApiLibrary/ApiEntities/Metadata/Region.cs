using ApiLibrary.ApiEntities.Attributes;
using ApiLibrary.ApiEntities.Base;
using Newtonsoft.Json;

namespace ApiLibrary.ApiEntities.Metadata
{
    [JsonPropertyMap(MappedJsonObjectName = "region", MappedJsonArrayName = "regions")]
    public class Region : IEntity
    {
        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
