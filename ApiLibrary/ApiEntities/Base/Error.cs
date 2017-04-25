using Newtonsoft.Json;

namespace ApiLibrary.ApiEntities.Base
{
    public class Error
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
