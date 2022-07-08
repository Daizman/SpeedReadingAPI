namespace SpeedReading.Application.Common.Interfaces
{
	public interface IAuthService
	{
		Task<UserAuthResponseDto> Authanticate(UserAuthRequestDto request, string ipAddress);
		Task<UserAuthResponseDto> RefreshToken(string token, string ipAddress);
		Task RevokeToken(string token, string ipAddress);
	}
}
