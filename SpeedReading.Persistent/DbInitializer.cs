using SpeedReading.Domain.Localization;

namespace SpeedReading.Persistent
{
	public static class DbInitializer
	{
		#region Languages
		public readonly static Language Ru = new()
		{
			Id = 1,
			Name = "Русский",
			Culture = "RU-ru"
		};
		public readonly static Language Eng = new()
		{
			Id = 2,
			Name = "English",
			Culture = "EN-en"
		};
		#endregion

		#region TasksGeneralNames
		public readonly static TaskGeneralName SchulteWithNumbersRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Таблицы Шульте (цифры)",
			Description = @"
				Это базовое упражнение, которое используется во многих курсах по скорочтению.
				Таблицу Шульте необходимо проходить, смотря строго в центр таблицы и
				периферийным, боковым зрением находить цифры в порядке их возрастания. Чем
				быстрее будут найдены все цифры в порядке их возрастания, тем лучше, но не гонитесь
				за скоростью, качество выполнения важнее.
				К тому же это упражнение улучшает зрение.
			",
			ProgramName = Domain.Task.Enums.TaskName.SchulteWithNumbers
		};
		public readonly static TaskGeneralName SchulteWithNumbersEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Schulte table (numbers)",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.SchulteWithNumbers
		};
		public readonly static TaskGeneralName SchulteWithLettersRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Таблицы Шульте (буквы)",
			Description = @"
				Это базовое упражнение, которое используется во многих курсах по скорочтению.
				Таблицу Шульте необходимо проходить, смотря строго в центр таблицы и
				периферийным, боковым зрением находить цифры в порядке их возрастания. Чем
				быстрее будут найдены все цифры в порядке их возрастания, тем лучше, но не гонитесь
				за скоростью, качество выполнения важнее.
				К тому же это упражнение улучшает зрение.
			",
			ProgramName = Domain.Task.Enums.TaskName.SchulteWithNumbers
		};
		public readonly static TaskGeneralName SchulteWithLettersEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Schulte table (letters)",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.SchulteWithNumbers
		};
		public readonly static TaskGeneralName MissingLettersReadingRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Исчезающие буквы",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.MissingLettersReading
		};
		public readonly static TaskGeneralName MissingLettersReadingEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Missing letters",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.MissingLettersReading
		};
		public readonly static TaskGeneralName MissingStringsReadingRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Исчезающие строки",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.MissingStringsReading
		};
		public readonly static TaskGeneralName MissingStringsReadingEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Missing string",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.MissingStringsReading
		};
		public readonly static TaskGeneralName TenToOneReadingRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Чтение со счетом от 10 до 0",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.TenToOneReading
		};
		public readonly static TaskGeneralName TenToOneReadingEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Missing letters",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.TenToOneReading
		};
		public readonly static TaskGeneralName UpDownTextReadingRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Перевернутый текст",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.UpDownTextReading
		};
		public readonly static TaskGeneralName UpDownTextReadingEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Up-down text",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.UpDownTextReading
		};
		public readonly static TaskGeneralName NinetyDegreeTextReadingRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Чтение повернутого текста",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.NinetyDegreeTextReading
		};
		public readonly static TaskGeneralName NinetyDegreeTextReadingEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Reading rotated text",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.NinetyDegreeTextReading
		};
		public readonly static TaskGeneralName VerticalReadingRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Вертикальное чтение",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.VerticalReading
		};
		public readonly static TaskGeneralName VerticalReadingEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Vertical reading",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.VerticalReading
		};
		public readonly static TaskGeneralName PrepareReadingRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Чтение с подготовкой",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.PrepareReading
		};
		public readonly static TaskGeneralName PrepareReadingEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Reading with preparation",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.PrepareReading
		};
		public readonly static TaskGeneralName ParagraphsThemesRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Определение тем параграфов",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.ParagraphsThemes
		};
		public readonly static TaskGeneralName ParagraphsThemesEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Find paragraph theme",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.ParagraphsThemes
		};
		public readonly static TaskGeneralName BetweenLinesReadingRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Чтение между строк",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.BetweenLinesReading
		};
		public readonly static TaskGeneralName BetweenLinesReadingEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Reading between lines",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.BetweenLinesReading
		};
		public readonly static TaskGeneralName TikTakReadingRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Бегущее чтение",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.TikTakReading
		};
		public readonly static TaskGeneralName TikTakReadingEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Running reading",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.TikTakReading
		};
		public readonly static TaskGeneralName ReverseLettersReadingRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Отраженные буквы",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.ReverseLettersReading
		};
		public readonly static TaskGeneralName ReverseLettersReadingEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Reversed letters",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.ReverseLettersReading
		};
		public readonly static TaskGeneralName LettersPyramidsRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Буквенные пирамиды",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.LettersPyramids
		};
		public readonly static TaskGeneralName LettersPyramidsEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Letters pyramids",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.LettersPyramids
		};
		public readonly static TaskGeneralName ColourNamingRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Название цветов",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.ColourNaming
		};
		public readonly static TaskGeneralName ColourNamingEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Colour naming",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.ColourNaming
		};
		public readonly static TaskGeneralName PictureMemorizeRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Запоминание картинки",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.PictureMemorize
		};
		public readonly static TaskGeneralName PictureMemorizeEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Picture memorize",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.PictureMemorize
		};
		public readonly static TaskGeneralName PicturePairsRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Пары картинок",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.PicturePairs
		};
		public readonly static TaskGeneralName PicturePairsEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Picture pairs",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.PicturePairs
		};
		#endregion

		public static async Task InitializeAsync(ApplicationDbContext context)
		{
			await context.Database.EnsureCreatedAsync();
			await DefaulInsert(context);
		}

		private static async Task DefaulInsert(ApplicationDbContext context)
		{
			await context.Languages.AddRangeAsync(new[]
			{
				Ru,
				Eng
			});
			await context.TasksGeneralNames.AddRangeAsync(new[]
			{
				SchulteWithNumbersRu,
				SchulteWithLettersRu,
				MissingLettersReadingRu,
				MissingStringsReadingRu,
				TenToOneReadingRu,
				UpDownTextReadingRu,
				NinetyDegreeTextReadingRu,
				VerticalReadingRu,
				PrepareReadingRu,
				ParagraphsThemesRu,
				BetweenLinesReadingRu,
				TikTakReadingRu,
				ReverseLettersReadingRu,
				LettersPyramidsRu,
				ColourNamingRu,
				PictureMemorizeRu,
				PicturePairsRu,
				SchulteWithNumbersEng,
				SchulteWithLettersEng,
				MissingLettersReadingEng,
				MissingStringsReadingEng,
				TenToOneReadingEng,
				UpDownTextReadingEng,
				NinetyDegreeTextReadingEng,
				VerticalReadingEng,
				PrepareReadingEng,
				ParagraphsThemesEng,
				BetweenLinesReadingEng,
				TikTakReadingEng,
				ReverseLettersReadingEng,
				LettersPyramidsEng,
				ColourNamingEng,
				PictureMemorizeEng,
				PicturePairsEng
			});

			await context.SaveChangesAsync();
		}
	}
}
