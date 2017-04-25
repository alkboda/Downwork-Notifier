using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ApiLibrary.ApiEntities.HR
{
    public enum UserStatus
    {
        [JsonConverter(typeof(StringEnumConverter), false)]
        Active,
        [JsonConverter(typeof(StringEnumConverter), false)]
        Inactive
    }
}
