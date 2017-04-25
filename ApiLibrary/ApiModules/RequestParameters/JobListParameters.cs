using ApiLibrary.ApiEntities.Attributes;
using ApiLibrary.ApiModules.Attributes;
using System;

namespace ApiLibrary.ApiModules.RequestParameters
{
    public class JobListParameters : RequestParameters
    {
        public JobListParameters() { }
        public JobListParameters(String buyerTeamReference)
        {
            if (String.IsNullOrWhiteSpace(buyerTeamReference))
            {
                throw new ArgumentNullException(nameof(buyerTeamReference));
            }

            BuyerTeamReference = buyerTeamReference;
        }

        [UrlParameter("buyer_team__reference")]
        public string BuyerTeamReference { get; set; }
        [UrlParameter("include_sub_teams")]
        public int IncludeSubTeams { get; set; }
        [UrlParameter("created_by")]
        public string CreatedBy { get; set; }
        [UrlParameter("status")]
        public string Status { get; set; } // open, filled, cancelled

        [UrlParameter("created_time_from")]
        public DateTime CreatedAfter { get; set; }
        [UrlParameter("created_time_to")]
        public DateTime CreatedBefore { get; set; }

        [UrlParameter("page")]
        public Paging Paging { get; set; }
        [UrlParameter("order_by")]
        public string OrderBy { get; set; }
    }
}
