using ApiLibrary.ApiEntities.Base;
using Newtonsoft.Json;
using System;

namespace ApiLibrary.ApiEntities.Profiles
{
    public class Client : IEntity
    {
        [JsonProperty("country")]
        public String Country { get; set; }

        [JsonProperty("feedback")]
        public double Feedback { get; set; }

        [JsonProperty("jobs_posted")]
        public int JobsPosted { get; set; }

        [JsonProperty("past_hires")]
        public int PastHires { get; set; }

        [JsonProperty("payment_verification_status")]
        public string PaymentVerificationStatus { get; set; }

        [JsonProperty("reviews_count")]
        public int ReviewsCount { get; set; }
    }
}
