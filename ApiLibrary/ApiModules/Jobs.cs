using ApiLibrary.ApiEntities.Base;
using ApiLibrary.ApiEntities.Profiles;
using ApiLibrary.ApiModules.Attributes;
using ApiLibrary.ApiModules.RequestParameters;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiLibrary.ApiModules
{
    public class Jobs : IApiModule
    {
        private Downwork _apiInstance = null;

        internal protected Jobs(Downwork apiInstance)
        {
            _apiInstance = apiInstance ?? throw new ArgumentNullException(nameof(apiInstance));
        }

        #region IApiModule interface implementation
        public string ApiNamespace { get; } = "hr";

        public Dictionary<string, string> MethodUris { get; } = new Dictionary<string, string>()
        {
            [nameof(Search)] = "search/jobs",
            [nameof(GetJobsList)] = "jobs"
        };
        #endregion IApiModule interface implementation

        #region Module API methods
        [ApiMethod(ApiMethod = "GET", ApiVersion = "v2", ApiNamespaceOverride = "profiles")]
        public Task<Answer<Job[]>> Search(JobSearchParameters parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            return _apiInstance.RequestPostAsync<Job[]>(this, parameters.ToUrlParameters(), cancellationToken: cancellationToken);
        }

        [ApiMethod(ApiMethod = "GET", ApiVersion = "v2")]
        public async Task<String[]> GetJobsList(String buyerTeam, CancellationToken cancellationToken = default(CancellationToken))
        {
            var urlParameters = new Dictionary<String, String>
            {
                ["buyer_team__reference"] = buyerTeam
            };
            var result = await _apiInstance.RequestPostAsync(this, urlParameters, cancellationToken: cancellationToken);
            return new String[0];
        }
        #endregion Module API methods
    }
}
