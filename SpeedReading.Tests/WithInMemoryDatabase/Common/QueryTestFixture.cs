﻿using AutoMapper;
using SpeedReading.Application.Common.Mapping;
using SpeedReading.Persistent;

namespace SpeedReading.Tests.WithInMemoryDatabase.Common
{
	public class QueryTestFixture : IAsyncDisposable
	{
		public ApplicationDbContext Context;
		public IMapper Mapper;

		public QueryTestFixture()
		{
			Context = ApplicationContextFactory.Create();

			MapperConfiguration config = new(conf =>
			{
				conf.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
			});
			Mapper = config.CreateMapper();
		}

		public async ValueTask DisposeAsync()
		{
			await ApplicationContextFactory.Destroy(Context);
		}
	}

	[CollectionDefinition("QueryCollection")]
	public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}