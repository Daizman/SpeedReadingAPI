using System.Security.Cryptography;
using System.Text;

namespace SpeedReading.Application.Common.Helpers
{
	public static class AuthHelper
	{
		private static readonly SHA256 _SHA256 = SHA256.Create();
		public static byte[] ComputePasswordHash(string password)
		{
			return _SHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
		}
	}
}
