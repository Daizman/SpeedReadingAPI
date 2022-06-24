using SpeedReading.Domain.Auth;
using SpeedReading.Domain.User;

namespace SpeedReading.Application.Common.Interfaces
{
	public interface IJwtUtils
	{
		string GenerateJwtToken(User user);
		Guid? ValidateJwtToken(string jwtToken);
		Task<RefreshToken> GenerateRefreshToken(string ipAddress);
	}
}
