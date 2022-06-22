using SpeedReading.Domain.Task.Enums;

namespace SpeedReading.Domain.Task
{
	public class TaskWithText : TrainingTask
	{
		public override TrainingTaskCategory Category => TrainingTaskCategory.TimeLimited;
		public Text Text { get; set; }
		public int SecondsToComplete { get; set; }
	}
}
