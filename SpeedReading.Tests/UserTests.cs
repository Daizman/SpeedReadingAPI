namespace SpeedReading.Tests
{
	public class UserTests
	{
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

		#region Authenticate
		[Fact]
		public async Task AuthenticateAsync_WithIncorrectAuthenticateData_ReturnsEmptyJwtToken()
		{
			
		}

		[Fact]
		public async Task AuthenticateAsync_WithCorrectAuthenticateData_ReturnsJwtToken()
		{
			
		}

		[Fact]
		public async Task RefreshTokenAsync_WithIncorrectRefreshToken_ReturnsEmptyJwtToken()
		{

		}

		[Fact]
		public async Task RefreshTokenAsync_WithCorrectRefreshToken_ReturnsJwtToken()
		{

		}

		[Fact]
		public async Task RevokeTokenAsync_WithoutToken_ReturnsBadRequest()
		{
			
		}

		[Fact]
		public async Task RevokeTokenAsync_WithToken_ReturnsOkMessage()
		{
			
		}

		[Fact]
		public async Task GetRefreshTokensAsync_WithCorrectUserId_ReturnsRefreshTokensList()
		{
			
		}
		#endregion

		#region UserStatistic
		[Fact]
		public async Task GetUserDailyStatistic_WithUnexistingUser_ReturnsNotFound()
		{
			
		}

		[Fact]
		public async Task GetUserDailyStatistic_WithExistingUser_ReturnsUserDailyStatistic()
		{
			
		}
		#endregion

		#region Helpers
		#endregion
	}
}