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

		public async Task<UserAuthResponseDto> Authanticate(UserAuthRequestDto request, string ipAddress)
		{
			User? user = await _context.Users.FirstOrDefaultAsync(u => u.Login == request.Login);
			
			if (user is null)
			{
				throw new UserNotFoundException();	
			}
			if (VerifyPassword(request.Password, user))
			{
				throw new IncorrectPasswordException();
			}

			var token = _jwtUtils.GenerateJwtToken(user);
			var refreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);
			user.RefreshTokens.Add(refreshToken);
			RemoveOldRefreshTokens(user);
			await UpdateUserTokens(user);

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

		private async Task UpdateUserTokens(User user)
		{
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
		}

		public async Task<UserAuthResponseDto> RefreshToken(string oldRefreshToken, string ipAddress)
		{
			User user = await GetUserByRefreshToken(oldRefreshToken);
			RefreshToken? refreshToken = GetRefreshTokenForUserToken(user, oldRefreshToken);

			if (refreshToken is null)
			{
				throw new TokenNotFoundException(user.Login);
			}
			if (refreshToken.IsRevoked)
			{
				RevokeDescendantRefreshToken(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {oldRefreshToken}");
				await UpdateUserTokens(user);
			}
			if (!refreshToken.IsActive)
			{
				throw new InvalidTokenException();
			}

			RefreshToken newRefreshToken = await RotateRefreshToken(refreshToken, ipAddress);
			user.RefreshTokens.Add(newRefreshToken);
			RemoveOldRefreshTokens(user);
			await UpdateUserTokens(user);

			return new()
			{
				Id = user.Id,
				Login = user.Login,
				JwtToken = _jwtUtils.GenerateJwtToken(user),
				RefreshToken = newRefreshToken.Token
			};
		}

		private async Task<User> GetUserByRefreshToken(string token)
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

		private async Task<RefreshToken> RotateRefreshToken(RefreshToken token, string ipAddress)
		{
			RefreshToken newRefreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);
			RevokeRefreshToken(token, ipAddress, "Replaced by new token", newRefreshToken.Token);
			return newRefreshToken;
		}

		public async Task RevokeToken(string token, string ipAddress)
		{
			User user = await GetUserByRefreshToken(token);
			RefreshToken? refreshToken = GetRefreshTokenForUserToken(user, token);

			if (refreshToken is null || !refreshToken.IsActive)
			{
				throw new InvalidTokenException();
			}

			RevokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
			await UpdateUserTokens(user);
		}
	}
}
