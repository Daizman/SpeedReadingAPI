using SpeedReading.Domain.Task.Enums;

namespace SpeedReading.Domain.Task
{
	public class PictureMemorize : TrainingTask
	{
		public override TrainingTaskCategory Category => TrainingTaskCategory.PicturesWithDescriptions;
		// base64 format
		public string Picture { get; set; }
		public string TrueDescription { get; set; }
		public List<string> Descriptions { get; set; } = new();
	}
}
