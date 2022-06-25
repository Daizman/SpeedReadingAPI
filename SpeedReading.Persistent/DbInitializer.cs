namespace SpeedReading.Persistent
{
	public static class DbInitializer
	{
		public static async Task Initialize(ApplicationDbContext context)
		{
			await context.Database.EnsureCreatedAsync();
		}
	}
}
