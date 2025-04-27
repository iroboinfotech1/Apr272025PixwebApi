using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.UserManagement
{
    public class Permission
    {
        public int PermssionId { get; set; }

        public int RoleId { get; set; }

        public bool IsConnectorManagement { get; set; }

        public bool IsGroupManagement { get; set; }

        public bool IsbookRoom { get; set; }

        public bool IsBookService { get; set; }

        public bool IsUserManagement { get; set; }
        public bool IsSpaceManagement { get; set; }

        public bool IsBookDesk { get; set; }

        public bool IsFindColleague { get; set; }

        public bool IsPlayerMangement { get; set; }

        public bool IsOrganizationManagement { get; set; }

        public bool IsBookParking { get; set; }

        public bool IsManageVisitor { get; set; }

        public bool IsSampleData1 { get; set; }

        public bool IsSampleData2 { get; set; }
    }

    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refresh_expires_in")]
        public int RefreshExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("id_token")]
        public string IdToken { get; set; }

        [JsonPropertyName("not-before-policy")]
        public int NotBeforePolicy { get; set; }

        [JsonPropertyName("session_state")]
        public string SessionState { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }
}

