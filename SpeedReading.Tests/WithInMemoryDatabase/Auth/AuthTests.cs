using SpeedReading.Persistent;
using SpeedReading.Tests.WithInMemoryDatabase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedReading.Tests.WithInMemoryDatabase.Auth
{
	[Collection("QueryCollection")]
	public class AuthTests
	{
		private readonly IJwtUtils _jwtUtils;
		private readonly IUserService _userService;
	}
}
