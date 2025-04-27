using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.UserManagement
{
    public class UserManagement : BaseClass
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }

        public string Joined { get; set; }

        public string SecretWord { get; set; }

        public string RepeatSecretWord { get; set; }
        public bool IsVisitor { get; set; } // Corrected property name

        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; } = null;

        [JsonPropertyName("lastName")]
        public string? LastName { get; set; } = null;
        
        [JsonPropertyName("mobileno")]
        public long mobileno { get; set; }
      
        [JsonPropertyName("idtype")]
        public string[] idtype { get; set; }

        [JsonPropertyName("idvalue")]
        public string[] idvalue { get; set; }

    }

    public class UserKeyCloack : BaseClass
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public long CreatedTimestamp { get; set; }
        public bool Enabled { get; set; }

    }

    public class BaseClass
    {
        public string Success { get; set; }

        public ErrorInfo ErrorInfoDesc { get; set; }

    }

    public class ErrorInfo
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
      public class UserPreferences
  {
      public string UserId { get; set; }

      public string? DefaultTimeZone { get; set; }

      public string[]? TimeZoneInterest { get; set; }

      public int? BuildingId { get; set; } // Make nullable if optional

	    public byte[]? UserImagebyte { get; set; }
	
	    public string? UserImage { get; set; }
	
	    public string[] PreferredWeekdays { get; set; }

  }
}
