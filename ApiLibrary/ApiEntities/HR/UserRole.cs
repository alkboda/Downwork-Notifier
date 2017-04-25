using ApiLibrary.ApiEntities.Attributes;
using ApiLibrary.ApiEntities.Base;
using Newtonsoft.Json;

namespace ApiLibrary.ApiEntities.HR
{
    [JsonPropertyMap(MappedJsonArrayName = "roles")]
    public class UserRole : IEntity
    {
        [JsonProperty("parent_team__id")]
        public string ParentTeamId { get; set; }
        [JsonProperty("user__first_name")]
        public string UserFirstName { get; set; }
        [JsonProperty("permissions")]
        public PermissionSet[] Permissions { get; set; }
        [JsonProperty("company__reference")]
        public int CompanyReference { get; set; }
        [JsonProperty("user__last_name")]
        public string UserLastName { get; set; }
        [JsonProperty("team__is_hidden")]
        public bool IsTeamHidden { get; set; }
        [JsonProperty("reference")]
        public int Reference { get; set; }
        [JsonProperty("team__reference")]
        public int TeamReference { get; set; }
        [JsonProperty("affiliation_status")]
        public string AffiliationStatus { get; set; }
        [JsonProperty("user__reference")]
        public int UserReference { get; set; }
        [JsonProperty("user__is_provider")]
        public bool IsUserProvider { get; set; }
        [JsonProperty("parent_team__name")]
        public string ParentTeamName { get; set; }
        [JsonProperty("has_team_room_access")]
        public bool HasTeamRoomAccess { get; set; }
        [JsonProperty("parent_team__reference")]
        public int ParentTeamReference { get; set; }
        [JsonProperty("team__id")]
        public string TeamId { get; set; }
        [JsonProperty("engagement__reference")]
        public string EngagementReference { get; set; }
        [JsonProperty("team__name")]
        public string TeamName { get; set; }
        [JsonProperty("company__name")]
        public string CompanyName { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("user__id")]
        public string UserId { get; set; }
        [JsonProperty("is_owner")]
        public bool IsOwner { get; set; }
    }
}
