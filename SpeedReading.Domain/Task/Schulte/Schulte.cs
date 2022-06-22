using SpeedReading.Domain.Task.Enums;

namespace SpeedReading.Domain.Task.Schulte
{
	public abstract class Schulte : TrainingTask
	{
		public override TrainingTaskCategory Category => TrainingTaskCategory.Schulte;
		public int Size { get; set; }
	}
}
