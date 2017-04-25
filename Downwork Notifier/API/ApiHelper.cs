using Downwork_Notifier.ViewModels.API.ApiEntities.Metadata;
using Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Downwork_Notifier.API
{
    public class ApiHelper
    {
        private string _accessToken = null;
        private string _accessTokenSecret = null;

        public ApiHelper(String accessToken, String accessTokenSecret)
        {
            _accessToken = accessToken;
            _accessTokenSecret = accessTokenSecret;
        }

        #region Properties
        public CategoryViewModel[] Categories { get; private set; }
        public String[] Skills { get; private set; }
        #endregion Properties

        public async Task<Tuple<String, String>> Init()
        {
            if (String.IsNullOrWhiteSpace(_accessToken) || String.IsNullOrWhiteSpace(_accessTokenSecret))
            {
                ApiLibrary.Downwork.Instance.Init(ApiKey.ApplicationKey, ApiKey.ApplicationSecret);

                var result = await ApiLibrary.Downwork.Instance.Auth.GetRequestToken();
                var s = ApiLibrary.Downwork.Instance.Auth.GetAuthorizationUrl(result?.Item1);
                if (result != null && !String.IsNullOrWhiteSpace(s))
                {
                    String oauthVerifier = null;
                    var verifierWindow = new VerifierCode();
                    verifierWindow.btnEnter.Click += (_s, _e) =>
                    {
                        oauthVerifier = verifierWindow.OAuthVerifier;
                        verifierWindow.Close();
                    };
                    System.Diagnostics.Process.Start(s);
                    verifierWindow.ShowDialog();

                    var accessTokenPair = await ApiLibrary.Downwork.Instance.Auth.GetAccessToken(result.Item1, result.Item2, oauthVerifier);
                    ApiLibrary.Downwork.Instance.OAuthToken = accessTokenPair.Item1;
                    ApiLibrary.Downwork.Instance.OAuthTokenSecret = accessTokenPair.Item2;

                    return accessTokenPair;
                }
                else
                {
                    throw new Exception("API messed up");
                }
            }
            else
            {
                ApiLibrary.Downwork.Instance.Init(ApiKey.ApplicationKey, ApiKey.ApplicationSecret, _accessToken, _accessTokenSecret);
                return new Tuple<String, String>(_accessToken, _accessTokenSecret);
            }
        }

        public async Task LoadMetadata()
        {
            var categories = await ApiLibrary.Downwork.Instance.Metadata.GetCategoriesV2();
            if (categories != null)
            {
                ApiLibrary.ApiEntities.Metadata.Category[] _UnionSubs(ApiLibrary.ApiEntities.Metadata.Category[] _categories)
                {
                    if (_categories?.Any() ?? false)
                    {
                        return _categories.Union(
                            _UnionSubs(
                                _categories.SelectMany(_ => _?.SubCategories ?? new ApiLibrary.ApiEntities.Metadata.Category[0]).ToArray()
                            )
                        ).ToArray();
                    }
                    else
                    {
                        return _categories;
                    }
                }
                var emptyCategory = new CategoryViewModel()
                {
                    Id = _UnionSubs(categories.Data).Min(_ => _.Id) - 1,
                    Title = null
                };
                Categories = new[] { emptyCategory }.Union(categories.Data.Select(_ => new CategoryViewModel(_))).ToArray();
            }

            var skills = await ApiLibrary.Downwork.Instance.Metadata.GetSkills();
            Skills = skills?.Data;

            //var categories = await ApiLibrary.Downwork.Instance.Metadata.GetRegions();
        }

        public async Task<ApiLibrary.ApiEntities.Profiles.Job[]> LoadJobs(JobSearchParametersViewModel searchParameters)
        {
            var answer = await ApiLibrary.Downwork.Instance.Jobs.Search(searchParameters.Entity);
            return answer?.Data;
        }
    }
}
