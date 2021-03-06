namespace SpeedReading.Persistent
{
	public static class DbInitializer
	{
		public static async Task InitializeAsync(ApplicationDbContext context)
		{
			await context.Database.EnsureCreatedAsync();
		}
	}
}
