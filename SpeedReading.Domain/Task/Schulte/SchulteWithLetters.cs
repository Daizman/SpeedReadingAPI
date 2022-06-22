using SpeedReading.Domain.Task.Enums;

namespace SpeedReading.Domain.Task.Schulte
{
	public class SchulteWithLetters : Schulte
	{
		public List<char> Letters { get; set; } = new();
	}
}
