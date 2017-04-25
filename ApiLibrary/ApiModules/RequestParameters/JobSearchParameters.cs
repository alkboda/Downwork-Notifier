using ApiLibrary.ApiEntities.Const;
using ApiLibrary.ApiModules.Attributes;
using System;

namespace ApiLibrary.ApiModules.RequestParameters
{
    public class JobSearchParameters : RequestParameters
    {
        private JobSearchParameters() { }
        public JobSearchParameters(String query = null, String title = null, String skills = null)
        {
            if (String.IsNullOrWhiteSpace($"{query}{title}{skills}"))
            {
                //todo: undo
                //throw new ArgumentException("At least one of constructor parameters must have value");
            }

            Query = query;
            Title = title;
            Skills = skills;
        }

        [UrlParameter("q")]
        public string Query { get; set; }
        [UrlParameter("title")]
        public string Title { get; set; }
        [UrlParameter("skills")]
        public string Skills { get; set; }

        [UrlParameter("category2")]
        public string Category { get; set; }
        [UrlParameter("subcategory2")]
        public string Subcategory { get; set; }

        [UrlParameter("job_type")]
        public JobType JobType { get; set; }
        [UrlParameter("job_status")]
        public JobStatus JobStatus { get; set; }
        [UrlParameter("budget")]
        public RangeParameter<int> Budget { get; set; }
        [UrlParameter("workload")]
        public Workload Workload { get; set; }

        [UrlParameter("duration")]
        public Duration Duration { get; set; }
        [UrlParameter("days_posted")]
        public int DaysPosted { get; set; }

        [UrlParameter("client_feedback")]
        public RangeParameter<double> ClientFeedback { get; set; }
        [UrlParameter("client_hires")]
        public RangeParameter<int> ClientHires { get; set; }

        [UrlParameter("paging")]
        public Paging Paging { get; set; }
        [UrlParameter("sort")]
        public string Sort { get; set; }
    }
}
