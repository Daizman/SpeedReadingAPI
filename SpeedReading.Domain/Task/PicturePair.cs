﻿using SpeedReading.Domain.Task.Enums;

namespace SpeedReading.Domain.Task
{
	public class PicturePair : TrainingTask
	{
		public override TrainingTaskCategory Category => TrainingTaskCategory.OnlyPictures;
		public List<string> Pictures { get; set; } = new();
	}
}