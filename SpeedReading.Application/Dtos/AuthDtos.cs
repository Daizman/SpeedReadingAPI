using System.Text.Json.Serialization;

namespace SpeedReading.Application.Dtos
{
	public record UserAuthResponseDto
	{
		public Guid Id { get; init; }
		public string Login { get; init; }
		public string JwtToken { get; init; }
		[JsonIgnore]
		public string RefreshToken { get; init; }
	}

	public record UserAuthRequestDto(string Login, string Password);

	public record RevokeTokenRequestDto(string Token);
}
