using ApiLibrary.ApiEntities.Attributes;
using ApiLibrary.ApiEntities.Base;
using Newtonsoft.Json;
using System;

namespace ApiLibrary.ApiEntities.Metadata
{
    [JsonPropertyMap(MappedJsonObjectName = "skill", MappedJsonArrayName = "skills")]
    public class Skill : IEntity
    {
        [JsonProperty("skill")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("external_url")]
        public Uri DesciptionUrl { get; set; }
    }
}
