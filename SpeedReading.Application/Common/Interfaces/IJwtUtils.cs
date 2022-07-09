using SpeedReading.Domain.Auth;
using SpeedReading.Domain.User;

namespace SpeedReading.Application.Common.Interfaces
{
	public interface IJwtUtils
	{
		string GenerateJwtToken(User user);
		Task<Guid> ValidateJwtTokenAsync(string jwtToken);
		Task<RefreshToken> GenerateRefreshTokenAsync(string ipAddress);
	}
}
