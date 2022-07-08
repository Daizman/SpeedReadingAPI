using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using SpeedReading.Application.Common.Implementation;
using SpeedReading.Application.Common.Models;
using SpeedReading.Domain.Auth;
using SpeedReading.Persistent;
using SpeedReading.Tests.WithInMemoryDatabase.Common;

namespace SpeedReading.Tests.WithInMemoryDatabase.Auth
{
	[Collection("QueryCollection")]
	public class JwtUtilsTests
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IOptions<AppSettings> _settings;

		public JwtUtilsTests(QueryTestFixture testFixture) =>
			(_context, _mapper, _settings) = (testFixture.Context, testFixture.Mapper, testFixture.Settings);

		#region GenerateToken
		[Fact]
		public void GenerateJwtToken_WithUnexistingUser_ThrowsArgumentNull()
		{
			// Arrange
			var jwtUtils = new JwtUtils(_context, _mapper, _settings);
			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(() =>
			{
				_ = jwtUtils.GenerateJwtToken(null);
			});
		}

		[Fact]
		public void GenerateJwtToken_WithExistingUser_ReturnsString()
		{
			// Arrange
			var jwtUtils = new JwtUtils(_context, _mapper, _settings);
			// Act
			var result = jwtUtils.GenerateJwtToken(ApplicationContextFactory.UserA);
			// Assert
			result.Should().BeOfType<string>();
		}
		#endregion

		#region ValidateToken
		[Fact]
		public async Task ValidateJwtToken_WithEmptyJwtToken_ReturnsEmptyGuid()
		{
			// Arrange
			var jwtUtils = new JwtUtils(_context, _mapper, _settings);
			// Act
			var result = await jwtUtils.ValidateJwtToken(string.Empty);
			// Assert
			result.Should().BeEmpty();
		}

		[Fact]
		public async Task ValidateJwtToken_WithIncorrectJwtToken_ReturnsEmptyGuid()
		{
			// Arrange
			var jwtUtils = new JwtUtils(_context, _mapper, _settings);
			// Act
			var result = await jwtUtils.ValidateJwtToken("TestToken");
			// Assert
			result.Should().BeEmpty();
		}

		[Fact]
		public async Task ValidateJwtToken_WithCorrectJwtToken_ReturnsUserId()
		{
			// Arrange
			var jwtUtils = new JwtUtils(_context, _mapper, _settings);
			var token = jwtUtils.GenerateJwtToken(ApplicationContextFactory.UserA);
			// Act
			var result = await jwtUtils.ValidateJwtToken(token);
			// Assert
			result.Should().NotBeEmpty()
					   .And.Be(ApplicationContextFactory.UserA.Id);
		}
		#endregion

		#region GenerateRefreshToken
		[Fact]
		public async Task GenerateRefreshToken_WithAnyIpAddress_ReturnsRefreshToken()
		{
			// Arrange
			var jwtUtils = new JwtUtils(_context, _mapper, _settings);
			string ip = "0.0.0.0";
			// Act
			var result = await jwtUtils.GenerateRefreshToken(ip);
			// Assert
			result.Should().BeOfType<RefreshToken>();
			result.Ip.Should().Be(ip);
			result.Token.Should().NotBeNullOrEmpty();
			result.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
			result.ExpirationDate.Should().BeCloseTo(DateTime.UtcNow.AddDays(7), TimeSpan.FromSeconds(2));
		}
		#endregion
	}
}
