using ApiLibrary.ApiEntities.Attributes;
using ApiLibrary.ApiEntities.Base;
using Newtonsoft.Json;
using System;

namespace ApiLibrary.ApiEntities.Profiles
{
    [JsonPropertyMap(MappedJsonObjectName = "job", MappedJsonArrayName = "jobs")]
    public class Job : IEntity
    {
        [JsonProperty("id")]
        public String Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }
        [JsonProperty("snippet")]
        public String Snippet { get; set; }
        [JsonProperty("skills")]
        public string[] Skills { get; set; }
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("category2")]
        public string Category { get; set; }
        [JsonProperty("subcategory2")]
        public string Subcategory { get; set; }

        [JsonProperty("job_type")]
        public JobType JobType { get; set; }
        [JsonProperty("job_status")]
        public string JobStatus { get; set; }
        [JsonProperty("budget")]
        public int Budget { get; set; }
        [JsonProperty("workload")]
        public string Workload { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("client")]
        public Client Client { get; set; }
    }
}
