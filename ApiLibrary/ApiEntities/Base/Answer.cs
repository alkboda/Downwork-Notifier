using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace ApiLibrary.ApiEntities.Base
{
    public class Answer<TEntity> where TEntity : class
    {
        [JsonProperty("server_time")]
        //[JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public long ServerTime { get; set; }
        
        [JsonProperty("auth_user")]
        public Object AuthUser { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }

        public TEntity Data { get; set; }

        [JsonProperty("profile_access")]
        public String ProfileAccess { get; set; }

        [JsonProperty("paging")]
        public ApiModules.RequestParameters.Paging Paging { get; set; }
    }
}
