namespace SpeedReading.Application.Common.Interfaces
{
	public interface IAuthService
	{
		Task<UserAuthResponseDto> AuthenticateAsync(UserAuthRequestDto request, string ipAddress);
		Task<UserAuthResponseDto> RefreshTokenAsync(string token, string ipAddress);
		Task RevokeTokenAsync(string token, string ipAddress);
	}
}
