using AutoMapper;
using Microsoft.Extensions.Options;
using SpeedReading.Application.Common.Implementation;
using SpeedReading.Application.Common.Mapping;
using SpeedReading.Application.Common.Models;
using SpeedReading.Persistent;

namespace SpeedReading.Tests.WithInMemoryDatabase.Common
{
	public class QueryTestFixture : IAsyncDisposable
	{
		public ApplicationDbContext Context;
		public IMapper Mapper;
		public IOptions<AppSettings> Settings;
		public IJwtUtils JwtUtils;

		public QueryTestFixture()
		{
			Context = ApplicationContextFactory.Create();

			MapperConfiguration config = new(conf =>
			{
				conf.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
			});
			Mapper = config.CreateMapper();

			Settings = Options.Create<AppSettings>(new()
			{
				Secret = "Secret with at least 128 bits",
				RefreshTokenTTL = 2
			});

			JwtUtils = new JwtUtils(Context, Mapper, Settings);
		}

		public async ValueTask DisposeAsync()
		{
			await ApplicationContextFactory.Destroy(Context);
		}
	}

	[CollectionDefinition("QueryCollection")]
	public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
