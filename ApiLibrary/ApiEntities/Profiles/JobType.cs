using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ApiLibrary.ApiEntities.Profiles
{
    public enum JobType
    {
        [JsonConverter(typeof(StringEnumConverter))]
        Fixed,
        [JsonConverter(typeof(StringEnumConverter))]
        Hourly
    }
}
