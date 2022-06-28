namespace SpeedReading.Application.Common.Exceptions
{
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException() :
			base($"User with this identifier not found")
		{ }
	}
}
