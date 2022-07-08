using AutoMapper;
using Microsoft.Extensions.Options;
using SpeedReading.Application.Common.Exceptions;
using SpeedReading.Application.Common.Implementation;
using SpeedReading.Application.Common.Models;
using SpeedReading.Application.Dtos;
using SpeedReading.Persistent;
using SpeedReading.Tests.WithInMemoryDatabase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedReading.Tests.WithInMemoryDatabase.Auth
{
	[Collection("QueryCollection")]
	public class AuthTests
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IJwtUtils _jwtUtils;
		private readonly IOptions<AppSettings> _settings;

		public AuthTests(QueryTestFixture testFixture) =>
			(_context, _mapper, _jwtUtils, _settings) = (testFixture.Context, testFixture.Mapper, testFixture.JwtUtils, testFixture.Settings);

		[Fact]
		public async Task Authanticate_WithIncorrectLogin_ThrowsUserNotFound()
		{
			// Arrange
			var authService = new AuthService(_context, _mapper, _jwtUtils, _settings);
			var authUser = new UserAuthRequestDto("userIncorrect", "password1");
			string ip = "0.0.0.0";
			// Act
			// Assert
			await Assert.ThrowsAsync<UserNotFoundException>(async () =>
			{
				await authService.Authanticate(authUser, ip);
			});
		}

		[Fact]
		public async Task Authanticate_WithIncorrectPassword_ThrowsIncorrectPassword()
		{
			
		}

		[Fact]
		public async Task Authanticate_WithCorrectRequest_ReturnsUserAuthResponse()
		{
			
		}
	}
}
