using SpeedReading.Application.Common.Exceptions;
using FluentAssertions;
using SpeedReading.Application.Dtos.User;
using SpeedReading.Domain.User;

namespace SpeedReading.Tests.MockedTests
{
	public class UserTests
	{
		private readonly Mock<IUserService> _userServiceStub = new();


		#region UserCRUD
		[Fact]
		public async Task GetUserAsync_WithUnexistingUser_ThrowsUserNotFound()
		{
			// Arrange
			_userServiceStub.Setup(repo => repo.GetUserAsync(It.IsAny<Guid>()))
				.ThrowsAsync(new UserNotFoundException());
			var userService = _userServiceStub.Object;

			// Act
			var act = async() => await userService.GetUserAsync(Guid.NewGuid());

			// Assert
			await Assert.ThrowsAsync<UserNotFoundException>(act);
		}

		[Fact]
		public async Task GetUserAsync_WithExistingUser_ReturnsExpectingUser()
		{
			// Arrange
			var expectedUser = CreateRandomUserDto();
			_userServiceStub.Setup(repo => repo.GetUserAsync(It.IsAny<Guid>()))
				.ReturnsAsync(expectedUser);
			var userService = _userServiceStub.Object;

			// Act
			var result = await userService.GetUserAsync(Guid.NewGuid());

			// Assert
			result.Should().BeEquivalentTo(expectedUser);
		}

		[Fact]
		public async Task GetUsersAsync_WithExistingUsers_ReturnsAllUsers()
		{
			// Arrange
			var expectedUsers = new UserListDto(new List<UserLookupDto>() { CreateRandomUserLookupDto(), CreateRandomUserLookupDto(), CreateRandomUserLookupDto() });
			_userServiceStub.Setup(repo => repo.GetUsersAsync())
				.ReturnsAsync(expectedUsers);
			var userService = _userServiceStub.Object;

			// Act
			var result = await userService.GetUsersAsync();

			// Assert
			result.Should().BeEquivalentTo(expectedUsers);
		}

		[Fact]
		public async Task CreateUserAsync_WithUserToCreate_ReturnsCreatedUser()
		{
			
		}

		[Fact]
		public async Task UpdateUserAsync_WithUnexistingUser_ReturnsNotFound()
		{
			
		}

		[Fact]
		public async Task UpdateUserAsync_WithOtherUser_ReturnsNotFound()
		{
			
		}

		[Fact]
		public async Task UpdateUserAsync_WithCurrentUser_ReturnsNoContent()
		{
			
		}

		[Fact]
		public async Task DeleteUserAsync_WithUnexistingUser_ReturnsNotFound()
		{
			
		}

		[Fact]
		public async Task DeleteUserAsync_WithOtherUser_ReturnsNotFound()
		{
			
		}

		[Fact]
		public async Task DeleteUserAsync_WithCurrentUser_ReturnsNoContent()
		{
			
		}
		#endregion

		#region Helpers
		private UserDto CreateRandomUserDto()
		{
			return new()
			{
				Id = Guid.NewGuid(),
				Login = Guid.NewGuid().ToString(),
				Email = Guid.NewGuid().ToString(),
				Avatar = string.Empty,
				FirstName = string.Empty,
				LastName = string.Empty
			};
		}

		private UserLookupDto CreateRandomUserLookupDto()
		{
			return new()
			{
				Id = Guid.NewGuid(),
				Login = Guid.NewGuid().ToString(),
				Email = Guid.NewGuid().ToString()
			};
		}
		#endregion
	}
}