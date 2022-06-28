using SpeedReading.Application.Dtos.User;
using SpeedReading.Domain.User;

namespace SpeedReading.Application.Common
{
	public static class UsersExtensions
	{
		public static UserDto AsDto(this User user)
		{
			return new UserDto
			{
				Id = user.Id,
				Login = user.Login,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Avatar = user.Avatar,
				RefreshTokens = user.RefreshTokens
			};
		}
	}
}
