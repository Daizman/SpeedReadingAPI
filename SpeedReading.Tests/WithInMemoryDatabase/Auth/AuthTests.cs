﻿using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using SpeedReading.Application.Common.Exceptions;
using SpeedReading.Application.Common.Implementation;
using SpeedReading.Application.Common.Models;
using SpeedReading.Application.Dtos;
using SpeedReading.Domain.Auth;
using Microsoft.EntityFrameworkCore;
using SpeedReading.Tests.WithInMemoryDatabase.Common;

namespace SpeedReading.Tests.WithInMemoryDatabase.Auth
{
	[Collection("QueryCollection")]
	public class AuthTests
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IJwtUtils _jwtUtils;
		private readonly IOptions<AppSettings> _settings;
		private readonly string _ipAddress = "0.0.0.0";

		public AuthTests(QueryTestFixture testFixture) =>
			(_context, _mapper, _jwtUtils, _settings) = (testFixture.Context, testFixture.Mapper, testFixture.JwtUtils, testFixture.Settings);

		[Fact]
		public async System.Threading.Tasks.Task AuthanticateAsync_WithIncorrectLogin_ThrowsUserNotFound()
		{
			// Arrange
			var authService = new AuthService(_context, _mapper, _jwtUtils, _settings);
			var authUser = new UserAuthRequestDto("userIncorrect", "password1");
			// Act
			// Assert
			await Assert.ThrowsAsync<UserNotFoundException>(async () =>
			{
				await authService.AuthenticateAsync(authUser, _ipAddress);
			});
		}

		[Fact]
		public async System.Threading.Tasks.Task AuthanticateAsync_WithIncorrectPassword_ThrowsIncorrectPassword()
		{
			// Arrange
			var authService = new AuthService(_context, _mapper, _jwtUtils, _settings);
			var authUser = new UserAuthRequestDto(ApplicationContextFactory.UserA.Login, "psss");
			// Act
			// Assert
			await Assert.ThrowsAsync<IncorrectPasswordException>(async () =>
			{
				await authService.AuthenticateAsync(authUser, _ipAddress);
			});
		}

		[Fact]
		public async System.Threading.Tasks.Task AuthanticateAsync_WithCorrectRequest_ReturnsUserAuthResponse()
		{
			// Arrange
			var authService = new AuthService(_context, _mapper, _jwtUtils, _settings);
			var authUser = new UserAuthRequestDto(ApplicationContextFactory.UserA.Login, "password1");
			// Act
			var result = await authService.AuthenticateAsync(authUser, _ipAddress);
			// Assert
			result.Should().BeOfType<UserAuthResponseDto>();
			result.Id.Should().Be(ApplicationContextFactory.UserA.Id);
			result.Login.Should().Be(ApplicationContextFactory.UserA.Login);
			result.JwtToken.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async System.Threading.Tasks.Task RefreshTokenAsync_WithIncorrectToken_ThrowsUserNotFound()
		{
			// Arrange
			var authService = new AuthService(_context, _mapper, _jwtUtils, _settings);
			var token = "asdsadsa";
			// Act
			// Assert
			await Assert.ThrowsAsync<UserNotFoundException>(async () =>
			{
				await authService.RefreshTokenAsync(token, _ipAddress);
			});
		}

		[Fact]
		public async System.Threading.Tasks.Task RefreshTokenAsync_WithCorrectToken_ReturnsUserAuthResponse()
		{
			// Arrange
			var authService = new AuthService(_context, _mapper, _jwtUtils, _settings);
			var token = (await authService.AuthenticateAsync(new(ApplicationContextFactory.UserA.Login, "password1"), _ipAddress)).RefreshToken;
			// Act
			var result = await authService.RefreshTokenAsync(token, _ipAddress);
			// Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<UserAuthResponseDto>();
			result.Id.Should().Be(ApplicationContextFactory.UserA.Id);
			result.Login.Should().Be(ApplicationContextFactory.UserA.Login);
			result.JwtToken.Should().NotBeNullOrEmpty();
		}
	}
}
