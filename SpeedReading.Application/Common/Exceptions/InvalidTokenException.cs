namespace SpeedReading.Application.Common.Exceptions
{
	public class InvalidTokenException : Exception
	{
		public InvalidTokenException() : base("Invalid token") { }
	}
}
