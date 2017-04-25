using ApiLibrary.ApiEntities.Base;
using Newtonsoft.Json;

namespace ApiLibrary.ApiEntities.Metadata
{
    [Attributes.JsonPropertyMap(MappedJsonObjectName = "category", MappedJsonArrayName = "categories")]
    public class Category : IEntity
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("topics")]
        public Category[] SubCategories { get; set; }
    }
}