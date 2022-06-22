using SpeedReading.Domain.Task.Enums;

namespace SpeedReading.Domain.Task
{
	public class LetterPyramids : TrainingTask
	{
		public override TrainingTaskCategory Category => TrainingTaskCategory.SeveralRepeats;
		public char Divider { get; set; }
		public List<string> Words { get; set; } = new();
		public int RepeatsCount { get; set; }
	}
}
