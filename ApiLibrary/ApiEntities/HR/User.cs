using ApiLibrary.ApiEntities.Base;
using ApiLibrary.ApiEntities.Converters;
using Newtonsoft.Json;
using System;

namespace ApiLibrary.ApiEntities.HR
{
    [Attributes.JsonPropertyMap(MappedJsonObjectName = "user", MappedJsonArrayName = "users")]
    public class User : IEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("reference")]
        public int Reference { get; set; }
        [JsonProperty("is_provider")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool IsProvider { get; set; }
        [JsonProperty("status")]
        public UserStatus Status { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }
        [JsonProperty("timezone_offset")]
        public int TimezoneOffset { get; set; }
        
        [JsonProperty("public_url")]
        public Uri PublicUrl { get; set; }
        [JsonProperty("profile_key")]
        public string ProfileKey { get; set; }
    }
}
