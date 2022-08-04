namespace SpeedReading.Application.Common.Exceptions
{
	public class TaskNotFoundException : Exception
	{
		public TaskNotFoundException() :
			base($"Task with this identifier not found")
		{ }

	}
}
