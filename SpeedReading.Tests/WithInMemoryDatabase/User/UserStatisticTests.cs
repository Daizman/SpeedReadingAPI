using AutoMapper;
using SpeedReading.Tests.WithInMemoryDatabase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedReading.Tests.WithInMemoryDatabase.User
{
	[Collection("QueryCollection")]
	public class UserStatisticTests
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public UserStatisticTests(QueryTestFixture testFixture) =>
			(_context, _mapper) = (testFixture.Context, testFixture.Mapper);


	}
}
