using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace SpeedReading.Domain.Auth
{
	[Owned]
	public class RefreshToken
	{
		[JsonIgnore]
		public Guid Id { get; set; }
		public string Token { get; set; }
		public DateTime ExpirationDate { get; set; }
		public DateTime CreationDate { get; set; }
		public string Ip { get; set; }
		public DateTime? RevokedDate { get; set; }
		public string IpRevoked { get; set; } = string.Empty;
		public string ReplacedByToken { get; set; } = string.Empty;
		public string RevokedReason { get; set; } = string.Empty;
		public bool IsExpired => DateTime.Now >= ExpirationDate;
		public bool IsRevoked => RevokedDate is not null;
		public bool IsActive => !IsRevoked && !IsExpired;
	}
}
