using SpeedReading.Domain.Task.Enums;

namespace SpeedReading.Domain.Task.Schulte
{
	public class SchulteWithNumbers : Schulte
	{
		public List<int> Numbers { get; set; } = new();
	}
}
