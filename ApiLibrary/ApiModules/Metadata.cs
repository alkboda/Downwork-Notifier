using ApiLibrary.ApiEntities.Base;
using ApiLibrary.ApiEntities.Metadata;
using ApiLibrary.ApiModules.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiLibrary.ApiModules
{
    public class Metadata : IApiModule
    {
        private Downwork _apiInstance = null;

        internal protected Metadata(Downwork apiInstance)
        {
            _apiInstance = apiInstance ?? throw new ArgumentNullException(nameof(apiInstance));
        }

        #region IApiModule interface implementation
        public string ApiNamespace { get; } = "profiles";

        public Dictionary<string, string> MethodUris { get; } = new Dictionary<string, string>()
        {
            [nameof(GetCategoriesV2)] = "metadata/categories",
            [nameof(GetSkills)] = "metadata/skills",
            [nameof(GetRegions)] = "metadata/regions",
            [nameof(GetTests)] = "metadata/tests",
            [nameof(GetReasons)] = "metadata/reasons"
        };
        #endregion IApiModule interface implementation

        #region Module API methods
        [ApiMethod(ApiMethod = "GET", ApiVersion = "v2")]
        public Task<Answer<Category[]>> GetCategoriesV2(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiInstance.RequestPostAsync<Category[]>(this, cancellationToken: cancellationToken);
        }

        [ApiMethod(ApiMethod = "GET", ApiVersion = "v1")]
        public async Task<Answer<String[]>> GetSkills(CancellationToken cancellationToken = default(CancellationToken))
        {
            var answer = await _apiInstance.RequestPostAsync(this, cancellationToken: cancellationToken);
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new ApiEntities.Base.EntityContractResolver("skills")
            };
            return JsonConvert.DeserializeObject<ApiEntities.Base.Answer<String[]>>(answer, settings);
        }

        [ApiMethod(ApiMethod = "GET", ApiVersion = "v1")]
        public Task<Answer<Region[]>> GetRegions(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiInstance.RequestPostAsync<Region[]>(this, cancellationToken: cancellationToken);
        }

        [ApiMethod(ApiMethod = "GET", ApiVersion = "v1")]
        public Task<Answer<Test[]>> GetTests(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiInstance.RequestPostAsync<Test[]>(this, cancellationToken: cancellationToken);
        }

        [ApiMethod(ApiMethod = "GET", ApiVersion = "v1")]
        public async Task<Answer<String[][]>> GetReasons(ReasonType reasonType, CancellationToken cancellationToken = default(CancellationToken))
        {
            var urlParameters = new Dictionary<String, String>
            {
                ["type"] = reasonType.ToString()
            };
            var answer = await _apiInstance.RequestPostAsync(this, urlParameters, cancellationToken: cancellationToken);
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new ApiEntities.Base.EntityContractResolver("reasons")
            };
            return JsonConvert.DeserializeObject<ApiEntities.Base.Answer<String[][]>>(answer, settings);
        }
        #endregion Module API methods
    }
}
