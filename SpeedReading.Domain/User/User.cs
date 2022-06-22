using SpeedReading.Domain.Auth;
using System.Text.Json.Serialization;

namespace SpeedReading.Domain.User
{
	public class User
	{
		public Guid Id { get; set; }
		public string Login { get; set; }
		[JsonIgnore]
		public byte[] Password { get; set; }
		public string Email { get; set; }
		// base64 format
		public string Avatar { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public DateTime RegistrationDate { get; set; }

		[JsonIgnore]
		public List<RefreshToken> RefreshTokens { get; set; }
	}
}
