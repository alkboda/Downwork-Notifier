using Newtonsoft.Json;

namespace ApiLibrary.ApiEntities.HR
{
    public class PermissionSet
    {
        [JsonProperty("permission")]
        public UserRolePermission[] Permissions { get; set; }
    }

    public enum UserRolePermission // todo: correct mapping
    {
        ManageFinance, //manage_finance
        ManageEmployment, //manage_employment
        ManageRecruiting, //manage_recruiting
        ManageTeamRoom //manage_teamroom
    }
}
