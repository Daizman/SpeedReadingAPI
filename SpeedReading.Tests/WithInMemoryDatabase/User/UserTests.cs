using AutoMapper;
using SpeedReading.Application.Common.Exceptions;
using SpeedReading.Application.Common.Implementation;
using SpeedReading.Persistent;
using SpeedReading.Tests.WithInMemoryDatabase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedReading.Tests.WithInMemoryDatabase.User
{
	[Collection("QueryCollection")]
	public class UserTests
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public UserTests(QueryTestFixture testFixture) =>
			(_context, _mapper) = (testFixture.Context, testFixture.Mapper);

		[Fact]
		public async Task GetUserAsync_WithUnexistingUser_ThrowsUserNotFound()
		{
			// Arrange
			var userService = new UserService(_context, _mapper);
			// Act
			var act = async() => await userService.GetUserAsync(Guid.NewGuid());
			// Assert
			await Assert.ThrowsAsync<UserNotFoundException>(act);
		}
	}
}
