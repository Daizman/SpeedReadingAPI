using SpeedReading.Domain.Task.Enums;

namespace SpeedReading.Domain.Task
{
	public abstract class TrainingTask
	{
		public Guid Id { get; set; }
		public string Title => GeneralName.Title;
		public string Description => GeneralName.Description;
		public TaskName ProgramName => GeneralName.ProgramName;
		public abstract TrainingTaskCategory Category { get; }
		public TrainingSkill Skill => ProgramName switch
		{
			TaskName.SchulteWithNumbers => TrainingSkill.PeripheralVision,
			TaskName.SchulteWithLetters => TrainingSkill.PeripheralVision,
			TaskName.VerticalReading => TrainingSkill.PeripheralVision,
			TaskName.LettersPyramids => TrainingSkill.PeripheralVision,
			TaskName.ParagraphsThemes => TrainingSkill.Memory,
			TaskName.PictureMemorize => TrainingSkill.Memory,
			TaskName.PicturePairs => TrainingSkill.Memory,
			TaskName.MissingLettersReading => TrainingSkill.ReadingSpeed,
			TaskName.MissingStringsReading => TrainingSkill.ReadingSpeed,
			TaskName.TenToOneReading => TrainingSkill.ReadingSpeed,
			TaskName.UpDownTextReading => TrainingSkill.ReadingSpeed,
			TaskName.NinetyDegreeTextReading => TrainingSkill.ReadingSpeed,
			TaskName.PrepareReading => TrainingSkill.ReadingSpeed,
			TaskName.BetweenLinesReading => TrainingSkill.ReadingSpeed,
			TaskName.TikTakReading => TrainingSkill.ReadingSpeed,
			TaskName.ReverseLettersReading => TrainingSkill.ReadingSpeed,
			TaskName.ColourNaming => TrainingSkill.ReadingSpeed,
			_ => throw new NotImplementedException()
		};

		public TaskGeneralName GeneralName { get; set; }
	}
}
