namespace SpeedReading.Application.Common.Exceptions
{
	public class TokenNotFoundException : Exception
	{
		public TokenNotFoundException(string login) : base($"For user: {login} token was not found") { }
	}
}
