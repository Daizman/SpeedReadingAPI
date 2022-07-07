using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SpeedReading.Application.Common.Exceptions;
using SpeedReading.Application.Common.Implementation;
using SpeedReading.Application.Dtos.User;
using SpeedReading.Persistent;
using SpeedReading.Tests.WithInMemoryDatabase.Common;

namespace SpeedReading.Tests.WithInMemoryDatabase.User
{
	[Collection("QueryCollection")]
	public class UserTests
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public UserTests(QueryTestFixture testFixture) =>
			(_context, _mapper) = (testFixture.Context, testFixture.Mapper);

		#region ReadOperations
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

		[Fact]
		public async Task GetUserAsync_WithExistingUser_ReturnsExpectingUser()
		{
			// Arrange
			var userService = new UserService(_context, _mapper);
			// Act
			var result = await userService.GetUserAsync(Guid.Parse("2CAAA47D-2B34-4285-A26E-E00DB43A7BD9"));
			// Assert
			result.Should().BeOfType<UserDto>();
			result.Should().BeEquivalentTo(ApplicationContextFactory.UserA, options => options.ComparingByMembers<UserDto>().ExcludingMissingMembers());
		}

		[Fact]
		public async Task GetUsersAsync_WithExistingUsers_ReturnsAllUsers()
		{
			// Arrange
			var userService = new UserService(_context, _mapper);
			// Act
			var result = await userService.GetUsersAsync();
			// Assert
			result.Should().BeOfType<UserListDto>();
			result.Users.Should().NotBeEmpty()
							 .And.HaveCount(_context.Users.Count());
		}
		#endregion

		#region CreateOperations
		[Fact]
		public async Task CreateUserAsync_WithExistingUserLogin_ThrowsUserAlreadyExists()
		{
			// Arrange
			var userService = new UserService(_context, _mapper);
			string login = "login1";
			string password = "createUserPassword";
			string email = "createUserEmail@mail.com";
			// Act
			// Assert
			await Assert.ThrowsAsync<UserAlreadyExistsException>(async () =>
			{
				await userService.CreateAsync(new CreateUserDto(login, password, email, null, null, null));
			});
		}

		[Fact]
		public async Task CreateUserAsync_WithExistingUserEmail_ThrowsUserAlreadyExists()
		{
			// Arrange
			var userService = new UserService(_context, _mapper);
			string login = "createUserLogin";
			string password = "createUserPassword";
			string email = "email1";
			// Act
			// Assert
			await Assert.ThrowsAsync<UserAlreadyExistsException>(async () =>
			{
				await userService.CreateAsync(new CreateUserDto(login, password, email, null, null, null));
			});
		}

		[Fact]
		public async Task CreateUserAsync_WithCreateUserDto_ReturnsCreatedUserGuid()
		{
			// Arrange
			var userService = new UserService(_context, _mapper);
			string login = "createUserLogin";
			string password = "createUserPassword";
			string email = "createUserEmail@mail.com";
			// Act
			var result = await userService.CreateAsync(new CreateUserDto(login, password, email, null, null, null));
			// Assert
			Assert.NotNull(
				await _context.Users.FirstOrDefaultAsync(user =>
					user.Id == result
					&& user.Login == login
					&& user.Password == userService.ComputePasswordHash(password)
					&& user.Email == email));
		}
		#endregion

		#region UpdateOperations
		[Fact]
		public async Task UpdateUserAsync_WithUnexistingUser_ThrowsUserNotFound()
		{
			// Arrange
			var userService = new UserService(_context, _mapper);
			string newLogin = "newLoginNewLogin";
			// Act
			// Assert
			await Assert.ThrowsAsync<UserNotFoundException>(async () =>
			{
				await userService.UpdateAsync(new UpdateUserDto(Guid.NewGuid(), newLogin, null, null, null, null, null));
			});
		}

		[Fact]
		public async Task UpdateUserAsync_WithExistingUser_ReturnsVoid()
		{
			// Arrange
			var userService = new UserService(_context, _mapper);
			string newLogin = "newLoginNewLogin";
			// Act
			await userService.UpdateAsync(new UpdateUserDto(ApplicationContextFactory.UserIdForEdit, newLogin, null, null, null, null, null));
			// Assert
			Assert.NotNull(
				await _context.Users.FirstOrDefaultAsync(user =>
					user.Id == ApplicationContextFactory.UserIdForEdit
					&& user.Login == newLogin));
		}
		#endregion

		#region DeleteOperations
		[Fact]
		public async Task DeleteUserAsync_WithUnexistingUser_ThrowsUserNotFound()
		{
			// Arrange
			var userService = new UserService(_context, _mapper);
			// Act
			// Assert
			await Assert.ThrowsAsync<UserNotFoundException>(async () =>
			{
				await userService.DeleteAsync(Guid.NewGuid());
			});
		}

		[Fact]
		public async Task DeleteUserAsync_WithExistingUser_ReturnsVoid()
		{
			// Arrange
			var userService = new UserService(_context, _mapper);
			// Act
			await userService.DeleteAsync(ApplicationContextFactory.UserIdForDelete);
			// Assert
			Assert.Null(
				await _context.Users.FirstOrDefaultAsync(user => user.Id == ApplicationContextFactory.UserIdForDelete));
		}
		#endregion
	}
}
