namespace SpeedReading.Tests
{
	public class UserTests
	{
		private readonly Mock<IUserService> _userServiceStub = new();


		#region UserCRUD
		[Fact]
		public async Task GetUserAsync_WithUnexistingUser_ReturnsNotFound()
		{

		}

		[Fact]
		public async Task GetUserAsync_WithExistingUser_ReturnsExpectingUser()
		{

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
		#endregion
	}
}