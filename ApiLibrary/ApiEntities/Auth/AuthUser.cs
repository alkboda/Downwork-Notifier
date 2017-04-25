using ApiLibrary.ApiEntities.Attributes;
using ApiLibrary.ApiEntities.Base;
using ApiLibrary.ApiEntities.Converters;
using Newtonsoft.Json;
using System;

namespace ApiLibrary.ApiEntities.Auth
{
    [JsonPropertyMap(MappedJsonObjectName = "info", MappedJsonArrayName = "infos")]
    public class AuthUser : IEntity
    {
        [JsonProperty("ref")]
        public int Reference { get; set; }
        [JsonProperty("profile_url")]
        public Uri ProfileUrl { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }
        [JsonProperty("capacity")]
        public Capacity Capacity { get; set; }

        [JsonProperty("has_agency")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool HasAgency { get; set; }
        [JsonProperty("company_url")]
        public Uri CompanyUrl { get; set; }

        [JsonProperty("portrait_32_img")]
        public Uri Portrait32Url { get; set; }
        [JsonProperty("portrait_50_img")]
        public Uri Portrait50Url { get; set; }
        [JsonProperty("portrait_100_img")]
        public Uri Portrait100Url { get; set; }
    }
}
