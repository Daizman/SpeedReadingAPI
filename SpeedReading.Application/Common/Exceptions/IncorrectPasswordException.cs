﻿namespace SpeedReading.Application.Common.Exceptions
{
	public class IncorrectPasswordException : Exception
	{
		public IncorrectPasswordException() : base("Incorrect password") { }
	}
}
