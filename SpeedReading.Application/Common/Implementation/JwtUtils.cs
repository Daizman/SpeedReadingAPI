using Microsoft.Extensions.Options;
using SpeedReading.Application.Common.Models;
using SpeedReading.Domain.Auth;
using SpeedReading.Domain.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SpeedReading.Application.Common.Implementation
{
	public class JwtUtils : BaseService, IJwtUtils
	{
		private readonly AppSettings _settings;

		public JwtUtils(IApplicationDbContext context, IMapper mapper, IOptions<AppSettings> settings) : base(context, mapper) => _settings = settings.Value;

		public string GenerateJwtToken(User user)
		{
			if (user is null)
			{
				throw new ArgumentNullException(nameof(user));
			}
			JwtSecurityTokenHandler tokenHandler = new();
			byte[] key = Encoding.UTF8.GetBytes(_settings.Secret);

			SecurityTokenDescriptor descriptor = new()
			{
				Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var securityToken = tokenHandler.CreateToken(descriptor);

			return tokenHandler.WriteToken(securityToken);
		}

		public async Task<RefreshToken> GenerateRefreshTokenAsync(string ipAddress)
		{
			return new()
			{
				Token = await GetUniqueTokenAsync(),
				ExpirationDate = DateTime.UtcNow.AddDays(7),
				CreationDate = DateTime.UtcNow,
				Ip = ipAddress
			};
		}

		private async Task<string> GetUniqueTokenAsync()
		{
			var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

			bool tokenExists = await _context.Users.AnyAsync(user => user.RefreshTokens.Any(rt => rt.Token == token));
			
			if(tokenExists)
			{
				return await GetUniqueTokenAsync();
			}
			return token;
		}
			
		public async Task<Guid> ValidateJwtTokenAsync(string jwtToken)
		{
			if (string.IsNullOrWhiteSpace(jwtToken))
			{
				return Guid.Empty;
			}

			JwtSecurityTokenHandler tokenHandler = new();
			byte[] key = Encoding.UTF8.GetBytes(_settings.Secret);
			try
			{
				var tokenValidationResult = await tokenHandler.ValidateTokenAsync(jwtToken, new()
															  {
																  ValidateIssuerSigningKey = true,
																  IssuerSigningKey = new SymmetricSecurityKey(key),
																  ValidateIssuer = false,
																  ValidateAudience = false,
																  ClockSkew = TimeSpan.Zero
															  });

				var jwtSecurityToken = tokenValidationResult.SecurityToken as JwtSecurityToken;
				Guid userId = Guid.Parse(jwtSecurityToken.Claims.First(x => x.Type == "id").Value);

				return userId;
			}
			catch
			{
				return Guid.Empty;
			}
		}
	}
}
