using SpeedReading.Application.Dtos;

namespace SpeedReading.Application.Common.Interfaces
{
	public interface IAuthService
	{
		Task<UserAuthResponseDto> Authanticate(UserAuthRequestDto requst, string ipAddress);
		Task<UserAuthResponseDto> RefreshToken(string token, string ipAddress);
		Task RevokeToken(string token, string ipAddress);
	}
}
