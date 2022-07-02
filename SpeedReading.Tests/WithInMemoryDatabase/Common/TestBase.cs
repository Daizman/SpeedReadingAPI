using SpeedReading.Persistent;

namespace SpeedReading.Tests.WithInMemoryDatabase.Common
{
	public abstract class TestBase : IAsyncDisposable
	{
		protected readonly ApplicationDbContext Context;

		public TestBase()
		{
			Context = ApplicationContextFactory.Create();
		}

		public async ValueTask DisposeAsync()
		{
			await ApplicationContextFactory.Destroy(Context);
		}
	}
}
