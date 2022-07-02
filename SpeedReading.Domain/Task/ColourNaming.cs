using SpeedReading.Domain.Task.Enums;
using SpeedReading.Domain.Task.OwnedTables;

namespace SpeedReading.Domain.Task
{
	public class ColourNaming : TrainingTask
	{
		public override TrainingTaskCategory Category => TrainingTaskCategory.WordsWithTags;
		public List<HexColour> ColourNamesHex { get; set; } = new();
	}
}
