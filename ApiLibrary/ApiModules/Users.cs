using ApiLibrary.ApiEntities.Base;
using ApiLibrary.ApiEntities.HR;
using ApiLibrary.ApiModules.Attributes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiLibrary.ApiModules
{
    public class Users : IApiModule
    {
        private Downwork _apiInstance = null;

        internal protected Users(Downwork apiInstance)
        {
            _apiInstance = apiInstance ?? throw new ArgumentNullException(nameof(apiInstance));
        }

        #region IApiModule interface implementation
        public string ApiNamespace { get; } = "hr";

        public Dictionary<string, string> MethodUris { get; } = new Dictionary<string, string>()
        {
            [nameof(GetUserByReference)] = "users/",
            [nameof(GetUserRoles)] = "userroles"
        };
        #endregion IApiModule interface implementation

        #region Module API methods
        [ApiMethod(ApiMethod = "GET", ApiVersion = "v2")]
        public Task<Answer<User>> GetUserByReference(String userReference = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            MethodUris[nameof(GetUserByReference)] = "users/" + (userReference ?? "me");
            return _apiInstance.RequestPostAsync<User>(this, cancellationToken: cancellationToken);
        }

        [ApiMethod(ApiMethod = "GET", ApiVersion = "v2")]
        public Task<Answer<UserRole[]>> GetUserRoles(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiInstance.RequestPostAsync<UserRole[]>(this, cancellationToken: cancellationToken);
        }
        #endregion Module API methods
    }
}
