using SpeedReading.Domain.Task.Enums;
using SpeedReading.Domain.Task.OwnedTables;

namespace SpeedReading.Domain.Task
{
	public class LetterPyramids : TrainingTask
	{
		public override TrainingTaskCategory Category => TrainingTaskCategory.SeveralRepeats;
		public char Divider { get; set; }
		public List<WordForLetterPyramid> Words { get; set; } = new();
		public int RepeatsCount { get; set; }
	}
}
