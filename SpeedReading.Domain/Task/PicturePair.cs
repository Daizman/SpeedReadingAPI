using SpeedReading.Domain.Task.Enums;
using SpeedReading.Domain.Task.OwnedTables;

namespace SpeedReading.Domain.Task
{
	public class PicturePair : TrainingTask
	{
		public override TrainingTaskCategory Category => TrainingTaskCategory.OnlyPictures;
		public List<Picture> Pictures { get; set; } = new();
	}
}
