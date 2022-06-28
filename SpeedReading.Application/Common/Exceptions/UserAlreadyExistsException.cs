namespace SpeedReading.Application.Common.Exceptions
{
	public class UserAlreadyExistsException : Exception
	{
		public UserAlreadyExistsException(string login, string email)
			: base($"User with login: {login}, or email: {email} already exists") { }
	}
}
