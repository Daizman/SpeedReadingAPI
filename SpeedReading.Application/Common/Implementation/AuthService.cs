using Microsoft.Extensions.Options;
using SpeedReading.Application.Common.Helpers;
using SpeedReading.Application.Common.Models;
using SpeedReading.Domain.Auth;
using SpeedReading.Domain.User;

namespace SpeedReading.Application.Common.Implementation
{
	public class AuthService : BaseService, IAuthService
	{
		private readonly IJwtUtils _jwtUtils;
		private readonly AppSettings _appSettings;

		public AuthService(IApplicationDbContext context, IMapper mapper, IJwtUtils jwtUtils, IOptions<AppSettings> appSettings) : base(context, mapper)
			=> (_jwtUtils, _appSettings) = (jwtUtils, appSettings.Value);

		public async Task<UserAuthResponseDto> AuthenticateAsync(UserAuthRequestDto request, string ipAddress)
		{
			User? user = await _context.Users.FirstOrDefaultAsync(u => u.Login == request.Login);
			
			if (user is null)
			{
				throw new UserNotFoundException();	
			}
			if (!VerifyPassword(request.Password, user))
			{
				throw new IncorrectPasswordException();
			}

			var token = _jwtUtils.GenerateJwtToken(user);
			var refreshToken = await _jwtUtils.GenerateRefreshTokenAsync(ipAddress);
			user.RefreshTokens.Add(refreshToken);
			RemoveOldRefreshTokens(user);
			await UpdateUserTokensAsync(user);

			return new()
			{
				Id = user.Id,
				Login = user.Login,
				JwtToken = token,
				RefreshToken = refreshToken.Token
			};
		}

		private bool VerifyPassword(string password, User loggingUser)
		{
			byte[] encodedPassword = AuthHelper.ComputePasswordHash(password);
			byte[] userPassword = loggingUser.Password;
			int passwordByteArrayLength = encodedPassword.Length;
			if (passwordByteArrayLength != userPassword.Length)
			{
				return false;
			}
			for (int i = 0; i < passwordByteArrayLength; i++)
			{
				if (encodedPassword[i] != userPassword[i])
				{
					return false;
				}
			}
			return true;
		}

		private void RemoveOldRefreshTokens(User user)
		{
			user.RefreshTokens.RemoveAll(x =>
				!x.IsActive &&
				x.CreationDate.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
		}

		private async Task UpdateUserTokensAsync(User user)
		{
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
		}

		public async Task<UserAuthResponseDto> RefreshTokenAsync(string oldRefreshToken, string ipAddress)
		{
			User user = await GetUserByRefreshTokenAsync(oldRefreshToken);
			RefreshToken refreshToken = GetRefreshTokenForUserToken(user, oldRefreshToken);  // Не может быть null из-за строки выше, упадем с UserNotFound

			if (refreshToken.IsRevoked)
			{
				RevokeDescendantRefreshToken(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {oldRefreshToken}");
				await UpdateUserTokensAsync(user);
			}
			if (!refreshToken.IsActive)
			{
				throw new InvalidTokenException();
			}

			RefreshToken newRefreshToken = await RotateRefreshTokenAsync(refreshToken, ipAddress);
			user.RefreshTokens.Add(newRefreshToken);
			RemoveOldRefreshTokens(user);
			await UpdateUserTokensAsync(user);

			return new()
			{
				Id = user.Id,
				Login = user.Login,
				JwtToken = _jwtUtils.GenerateJwtToken(user),
				RefreshToken = newRefreshToken.Token
			};
		}

		private async Task<User> GetUserByRefreshTokenAsync(string token)
		{
			User? user = await _context.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

			if (user is null)
			{
				throw new UserNotFoundException();
			}

			return user;
		}

		private RefreshToken? GetRefreshTokenForUserToken(User user, string token)
		{
			return user.RefreshTokens.SingleOrDefault(rt => rt.Token == token);
		}

		private void RevokeDescendantRefreshToken(RefreshToken refreshToken, User user, string ipAddress, string reason)
		{
			if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
			{
				RefreshToken? childToken = GetRefreshTokenForUserToken(user, refreshToken.ReplacedByToken);
				if (childToken is null)
					return;
				if (childToken.IsActive)
				{
					RevokeRefreshToken(childToken, ipAddress, reason);
				}
				else
				{
					RevokeDescendantRefreshToken(childToken, user, ipAddress, reason);
				}
			}
		}

		private void RevokeRefreshToken(RefreshToken token, string ipAddress, string? reason = null, string? replaceByToken = null)
		{
			token.RevokedDate = DateTime.UtcNow;
			token.IpRevoked = ipAddress;
			token.RevokedReason = reason ?? string.Empty;
			token.ReplacedByToken = replaceByToken ?? string.Empty;
		}

		private async Task<RefreshToken> RotateRefreshTokenAsync(RefreshToken token, string ipAddress)
		{
			RefreshToken newRefreshToken = await _jwtUtils.GenerateRefreshTokenAsync(ipAddress);
			RevokeRefreshToken(token, ipAddress, "Replaced by new token", newRefreshToken.Token);
			return newRefreshToken;
		}

		public async Task RevokeTokenAsync(string token, string ipAddress)
		{
			User user = await GetUserByRefreshTokenAsync(token);
			RefreshToken refreshToken = GetRefreshTokenForUserToken(user, token);  // Не может быть null из-за строки выше, упадем с UserNotFound

			if (!refreshToken.IsActive)
			{
				throw new InvalidTokenException();
			}

			RevokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
			await UpdateUserTokensAsync(user);
		}
	}
}
