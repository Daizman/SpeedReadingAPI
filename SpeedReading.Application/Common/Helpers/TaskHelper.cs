using SpeedReading.Domain.Task.Enums;

namespace SpeedReading.Application.Common.Helpers
{
	public static class TaskHelper
	{
		public static string TaskSkillToString(TrainingSkill skill)
			=> skill switch
			{
				TrainingSkill.ReadingSpeed => "Скорость чтения",
				TrainingSkill.Memory => "Память",
				TrainingSkill.PeripheralVision => "Переферийное зрение",
				_ => throw new NotImplementedException()
			};
	}
}
