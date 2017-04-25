using ApiLibrary.ApiEntities.Base;
using ApiLibrary.ApiModules.Attributes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiLibrary.ApiModules
{
    public class Auth : IApiModule
    {
        private Downwork _apiInstance = null;

        internal protected Auth(Downwork apiInstance)
        {
            _apiInstance = apiInstance ?? throw new ArgumentNullException(nameof(apiInstance));
        }

        #region IApiModule implementation
        public string ApiNamespace { get; } = "auth";

        public Dictionary<String, String> MethodUris { get; } = new Dictionary<string, string>()
        {
            [nameof(GetRequestToken)] = "oauth/token/request",
            [nameof(GetAccessToken)] = "oauth/token/access",
            [nameof(GetCurrentUser)] = "info"
        };
        #endregion IApiModule implementation

        #region Module helper methods
        public String GetAuthorizationUrl(String requestToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            Uri uri;
            if (Uri.TryCreate(_apiInstance.BaseUri, $"/services/api/auth?oauth_token={requestToken}", out uri))
            {
                return uri.ToString();
            }
            return null;
        }

        private bool ParseTokenAnswer(string answer, out string token, out string tokenSecret)
        {
            bool confirmed = true; token = tokenSecret = null;
            var parameters = answer.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            if (parameters.Length > 0)
            {
                foreach (var parameter in parameters)
                {
                    var pair = parameter.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (pair.Length == 2)
                    {
                        switch (pair[0].Trim().ToLower())
                        {
                            case "oauth_callback_confirmed":
                                if (!Boolean.TryParse(pair[1].Trim(), out confirmed))
                                {
                                    confirmed = false;
                                }
                                break;
                            case OAuth.OAuthBase.OAuthTokenKey:
                                token = pair[1].Trim();
                                break;
                            case OAuth.OAuthBase.OAuthTokenSecretKey:
                                tokenSecret = pair[1].Trim();
                                break;
                        }
                    }
                }
            }

            return (confirmed && !String.IsNullOrWhiteSpace(token) && !String.IsNullOrWhiteSpace(tokenSecret));
        }
        #endregion Module helper methods

        #region Module API methods
        [ApiMethod(ApiVersion = "v1", ApiMethod = "POST")]
        public async Task<Tuple<String, String>> GetRequestToken(CancellationToken cancellationToken = default(CancellationToken))
        {
            var answer = await _apiInstance.RequestPostAsync(this, cancellationToken: cancellationToken);

            String token = null; String tokenSecret = null;
            if (ParseTokenAnswer(answer, out token, out tokenSecret))
            {
                return new Tuple<string, string>(token, tokenSecret);
            }
            else
            {
                return null;
            }
        }

        [ApiMethod(ApiVersion = "v1", ApiMethod = "POST")]
        public async Task<Tuple<String, String>> GetAccessToken(String requestToken, String requestTokenSecret, String verifierCode, CancellationToken cancellationToken = default(CancellationToken))
        {
            _apiInstance.OAuthToken = requestToken;
            _apiInstance.OAuthTokenSecret = requestTokenSecret;
            var urlParameters = new Dictionary<string, string>
            {
                [OAuth.OAuthBase.OAuthVerifierKey] = verifierCode
            };

            var answer = await _apiInstance.RequestPostAsync(this, urlParameters, cancellationToken: cancellationToken);

            String token = null; String tokenSecret = null;
            if (ParseTokenAnswer(answer, out token, out tokenSecret))
            {
                return new Tuple<string, string>(token, tokenSecret);
            }
            else
            {
                return null;
            }
        }

        [ApiMethod(ApiMethod = "GET", ApiVersion = "v1")]
        public Task<Answer<ApiEntities.Auth.AuthUser>> GetCurrentUser(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiInstance.RequestPostAsync<ApiEntities.Auth.AuthUser>(this, cancellationToken: cancellationToken);
        }
        #endregion Module API methods
    }
}
