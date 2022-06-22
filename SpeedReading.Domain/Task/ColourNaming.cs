using SpeedReading.Domain.Task.Enums;

namespace SpeedReading.Domain.Task
{
	public class ColourNaming : TrainingTask
	{
		public override TrainingTaskCategory Category => TrainingTaskCategory.WordsWithTags;
		public Dictionary<string, string> FakeColourNameColourHex { get; set; }
	}
}
