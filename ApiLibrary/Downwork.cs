using ApiLibrary.ApiEntities.Base;
using ApiLibrary.ApiModules;
using ApiLibrary.ApiModules.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiLibrary
{
    public class Downwork
    {
        #region Singletone implementation
        private Downwork() { }
        private static Downwork _instance = null;
        public static Downwork Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Downwork();

                    _instance.Auth = new Auth(_instance);
                    _instance.Users = new Users(_instance);
                    _instance.Jobs = new Jobs(_instance);
                    _instance.Metadata = new Metadata(_instance);
                }
                return _instance;
            }
        }
        #endregion Singletone implementation

        #region Modules
        // note: Here is a point to rewrite to T4 template
        public ApiModules.Auth Auth { get; private set; }
        public ApiModules.Users Users { get; private set; }
        public ApiModules.Jobs Jobs { get; private set; }
        public ApiModules.Metadata Metadata { get; private set; }
        #endregion Modules

        #region Settings properties
        public Uri BaseUri { get; set; } = new Uri("https://www.upwork.com/");

        public String ConsumerToken { get; private set; } = null;
        public String ConsumerSecret { get; private set; } = null;
        public String OAuthToken { get; set; } = null;
        public String OAuthTokenSecret { get; set; } = null;
        #endregion Settings properties

        #region Init
        public void Init(String consumerKey, String consumerSecret, String accessToken = null, String accessTokenSecret = null)
        {
            if (String.IsNullOrWhiteSpace(consumerKey))
            {
                throw new ArgumentNullException(nameof(consumerKey));
            }
            if (String.IsNullOrWhiteSpace(consumerSecret))
            {
                throw new ArgumentNullException(nameof(consumerSecret));
            }

            ConsumerToken = consumerKey;
            ConsumerSecret = consumerSecret;
            if (!String.IsNullOrWhiteSpace(accessToken) && !String.IsNullOrWhiteSpace(accessTokenSecret))
            {
                OAuthToken = accessToken;
                OAuthTokenSecret = accessTokenSecret;
            }
        }
        #endregion Init

        #region inner request funtions
        /// <summary>
        /// Method takes all needed information and tries to build Uri for request to API
        /// </summary>
        /// <param name="module">API module, that calls request</param>
        /// <param name="methodName">Caller method name of API module</param>
        /// <param name="requestUri">Resulting Uri</param>
        /// <returns>Was building successful or not</returns>
        private Boolean BuildApiUrl(IApiModule module, String methodName, out Uri requestUri, out String httpMethod)
        {
            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }
            if (String.IsNullOrWhiteSpace(methodName))
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            var methodInfo = GetMethodInfo(module, methodName);
            httpMethod = methodInfo?.ApiMethod ?? "POST"; // lets assume default method for us is POST
            if (!String.IsNullOrWhiteSpace(methodInfo?.ApiUri))
            {
                // Predefined URI for method - for now 1 method: GetAuthorizationUrl
                //
                return Uri.TryCreate(BaseUri, methodInfo.ApiUri, out requestUri);
            }
            else
            {
                if (!module.MethodUris.ContainsKey(methodName))
                {
                    throw new ArgumentException("Unrecognized module's method name", nameof(methodName));
                }

                String methodNamespace = methodInfo?.ApiNamespaceOverride != null
                    ? methodInfo.ApiNamespaceOverride
                    : (module.ApiNamespace?.Trim(new[] { '/' }) ?? String.Empty);
                String apiVersion = methodInfo?.ApiVersion ?? String.Empty;
                String methodUri = module.MethodUris[methodName]?.Trim(new[] { '/' }) ?? String.Empty;

                String endpointUri = String.Join("/",
                    new[] {
                        "/api",
                        methodNamespace,
                        apiVersion,
                        methodUri
                    }.Where(_ => !String.IsNullOrWhiteSpace(_))
                );
                return Uri.TryCreate(BaseUri, endpointUri, out requestUri);
            }
        }

        /// <summary>
        /// Takes <see cref="ApiMethodAttribute"/> from module's method by name
        /// </summary>
        /// <param name="module">Caller module</param>
        /// <param name="methodName">Caller method name</param>
        /// <returns>Instance of <see cref="ApiMethodAttribute"/></returns>
        private ApiMethodAttribute GetMethodInfo(IApiModule module, String methodName)
        {
            if (module != null && !String.IsNullOrWhiteSpace(methodName))
            {
                return module.GetType().GetTypeInfo().GetDeclaredMethod(methodName)?.GetCustomAttribute(typeof(ApiMethodAttribute), false) as ApiMethodAttribute;
            }
            return null;
        }

        internal async protected Task<ApiEntities.Base.Answer<TEntity>> RequestPostAsync<TEntity>(IApiModule module, Dictionary<String, String> urlParameters = null, Object postData = null, [CallerMemberName]String moduleMethodName = "", CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class
        {
            var answer = await RequestPostAsync(module, urlParameters, postData, moduleMethodName, cancellationToken);
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new ApiEntities.Base.EntityContractResolver<TEntity>()
            };
            return JsonConvert.DeserializeObject<ApiEntities.Base.Answer<TEntity>>(answer, settings);
        }
        internal async protected Task<String> RequestPostAsync(IApiModule module, Dictionary<String, String> urlParameters = null, Object postData = null, [CallerMemberName]String moduleMethodName = "", CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!BuildApiUrl(module, moduleMethodName, out var requestUri, out var httpMethod))
            {
                throw new ArgumentException("Malformed endpoint URI");
            }

            //if (postData != null)
            //{
            //    requestUri = new Uri($"{requestUri.AbsolutePath}.json");
            //}

            // Fill url parameters
            //
            if (urlParameters != null && urlParameters.Count > 0) //note: here's some point to check method on GET
            {
                var urlParametersString = String.Join("&", urlParameters.Select(_ => $"{_.Key}={_.Value}"));
                requestUri = new Uri($"{requestUri.AbsoluteUri}?{urlParametersString}");
            }
            System.Diagnostics.Debug.WriteLineIf(System.Diagnostics.Debugger.IsAttached, $"Request API to: {requestUri}");
            requestUri = MakeOAuthRequest(requestUri, httpMethod);

            // Setup web request
            //
            cancellationToken.ThrowIfCancellationRequested();
            var request = (HttpWebRequest)HttpWebRequest.Create(requestUri);
            request.Accept = "application/json";
            request.Method = httpMethod;

            // Post the data
            //
            if (postData != null)
            {
                request.ContentType = "application/json";
                Task<Stream> requestStreamTask = request.GetRequestStreamAsync();
                cancellationToken.ThrowIfCancellationRequested();

                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    ContractResolver = new ParametersContractResolver(),
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };
                var dataBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(postData, settings));

                using (Stream requestStream = await requestStreamTask)
                {
                    await requestStream.WriteAsync(dataBytes, 0, dataBytes.Length, cancellationToken);
                    await requestStream.FlushAsync(cancellationToken);
                }
            }

            // Receive response
            //
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                using (var response = await request.GetResponseAsync())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            var answer = await streamReader.ReadToEndAsync();
                            return answer;
                        }
                    }
                }
            }
            catch (WebException webEx) when (ParseWebException(webEx))
            {
                using (var errorResponse = (HttpWebResponse)webEx.Response)
                {
                    switch (errorResponse.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                        case HttpStatusCode.Unauthorized:
                        case HttpStatusCode.Forbidden:
                        case HttpStatusCode.NotFound:
                        case HttpStatusCode.RequestEntityTooLarge:
                            // note: something more detailed here could be here
                            break;
                    }

                    using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                    {
                        string error = await reader.ReadToEndAsync();
                        return error;
                    }
                }
            }
            catch (WebException webEx)
            {
                using (var errorResponse = (HttpWebResponse)webEx.Response)
                {
                    var answer = new Answer<Object>
                    {
                        Error = new Error
                        {
                            Code = (int)errorResponse.StatusCode,
                            Message = errorResponse.StatusDescription
                        }
                    };
                    return JsonConvert.SerializeObject(answer);
                }
            }
            catch (Exception ex)
            {
                var answer = new Answer<Object>
                {
                    Error = new Error
                    {
                        Code = ex.HResult,
                        Message = $"{ex.Message}{Environment.NewLine}{ex.StackTrace}"
                    }
                };
                return JsonConvert.SerializeObject(answer);
            }
        }

        private Boolean ParseWebException(WebException webException)
        {
            var response = webException.Response as HttpWebResponse;
            if (response != null)
            {
                if (!String.IsNullOrEmpty(response.Headers["X-Upwork-Error-Code"])
                    || !String.IsNullOrEmpty(response.Headers["X-Upwork-Error-Message"]))
                {
                    return true;
                }
            }
            return false;
        }

        private Uri MakeOAuthRequest(Uri requestUri, String httpMethod)
        {
            var OAuthHelper = new OAuth.OAuthBase();
            var oauthNonce = OAuthHelper.GenerateNonce();
            var oauthTS = OAuthHelper.GenerateTimeStamp();

            String hmac = OAuthHelper.GenerateSignature(requestUri, ConsumerToken, ConsumerSecret, OAuthToken, OAuthTokenSecret, httpMethod,
                                                            oauthTS, oauthNonce, out var normalizedUrl, out var normalizedRequestParameters);
            
            return new Uri($"{normalizedUrl}?{normalizedRequestParameters}&{OAuth.OAuthBase.OAuthSignatureKey}={hmac}");
        }
        #endregion inner request funtions
    }
}
