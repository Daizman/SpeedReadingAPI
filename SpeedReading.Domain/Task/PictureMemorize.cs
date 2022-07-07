using SpeedReading.Domain.Task.Enums;
using SpeedReading.Domain.Task.OwnedTables;

namespace SpeedReading.Domain.Task
{
	public class PictureMemorize : TrainingTask
	{
		public override TrainingTaskCategory Category => TrainingTaskCategory.PicturesWithDescriptions;
		// base64 format
		public Picture Picture { get; set; }
		public PictureDescription TrueDescription { get; set; }
		public List<PictureDescription> Descriptions { get; set; } = new();
	}
}
