using Microsoft.EntityFrameworkCore;
using SpeedReading.Application.Common.Helpers;
using SpeedReading.Domain.Localization;
using SpeedReading.Persistent;

namespace SpeedReading.Tests.WithInMemoryDatabase.Common
{
	public class ApplicationContextFactory
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

		#region Users
		public readonly static Domain.User.User UserA = new()
		{
			Id = Guid.Parse("2CAAA47D-2B34-4285-A26E-E00DB43A7BD9"),
			RegistrationDate = DateTime.Today,
			Email = "email1",
			Login = "login1",
			Password = AuthHelper.ComputePasswordHash("password1"),
			Avatar = "base64(avatar)",
			FirstName = "Alex",
			LastName = "Tom",
			Broadcasting = false,
			UserInterfaceLanguageId = Ru.Id
		};
		public readonly static Domain.User.User UserB = new()
		{
			Id = Guid.Parse("7037CC33-7435-4316-B3AF-4FA3907CCEB3"),
			RegistrationDate = DateTime.Today,
			Email = "email2",
			Login = "login2",
			Password = AuthHelper.ComputePasswordHash("password2"),
			Avatar = string.Empty,
			FirstName = string.Empty,
			LastName = string.Empty,
			Broadcasting = false,
			UserInterfaceLanguageId = Eng.Id
		};

		public readonly static Guid UserIdForEdit = Guid.NewGuid();
		public readonly static Guid UserIdForDelete = Guid.NewGuid();
		#endregion

		#region Texts
		public readonly static Domain.Text TextRu = new()
		{
			Id = Guid.Parse("5691048C-CD89-4DCF-BBA5-66E32F4CFFCC"),
			LanguageId = Ru.Id,
			Title = "Просто текст",
			Description = "Это простой текст на русском языке",
			Content = "Текст на русском языке...Текст на русском языке...Текст на русском языке..."
		};
		public readonly static Domain.Text TextEng = new()
		{
			Id = Guid.Parse("FB04645C-FF52-43C9-AAEE-6A4B959A2922"),
			LanguageId = Eng.Id,
			Title = "Interesting text",
			Description = "It is VERY interesting text on english language",
			Content = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus semper quis lacus non viverra. Aenean interdum est tellus, ornare vestibulum quam porta id. Nunc feugiat, dui sit amet fermentum ullamcorper, metus est placerat ipsum, vel porttitor mauris est eu justo. Aenean vel iaculis purus. Interdum et malesuada fames ac ante ipsum primis in faucibus. Duis magna risus, congue vel nunc at, sagittis elementum ante. Pellentesque finibus turpis ac dapibus malesuada. Aliquam odio neque, molestie in magna ut, tincidunt lacinia tellus."
		};
		#endregion

		#region WordsForLetterPyramid
		public readonly static Domain.Task.OwnedTables.WordForLetterPyramid WordRu1 = new()
		{
			Id = 1,
			LanguageId = Ru.Id,
			Word = "привет"
		};
		public readonly static Domain.Task.OwnedTables.WordForLetterPyramid WordRu2 = new()
		{
			Id = 2,
			LanguageId = Ru.Id,
			Word = "рама"
		};
		public readonly static Domain.Task.OwnedTables.WordForLetterPyramid WordRu3 = new()
		{
			Id = 3,
			LanguageId = Ru.Id,
			Word = "лупа"
		};
		public readonly static Domain.Task.OwnedTables.WordForLetterPyramid WordEng1 = new()
		{
			Id = 4,
			LanguageId = Eng.Id,
			Word = "peace"
		};
		public readonly static Domain.Task.OwnedTables.WordForLetterPyramid WordEng2 = new()
		{
			Id = 5,
			LanguageId = Eng.Id,
			Word = "father"
		};
		public readonly static Domain.Task.OwnedTables.WordForLetterPyramid WordEng3 = new()
		{
			Id = 6,
			LanguageId = Eng.Id,
			Word = "race"
		};
		#endregion

		#region TasksGeneralNames
		public readonly static Domain.Task.TaskGeneralName SchulteWithNumbersRu = new()
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
		public readonly static Domain.Task.TaskGeneralName SchulteWithNumbersEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Schulte table (numbers)",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.SchulteWithNumbers
		};
		public readonly static Domain.Task.TaskGeneralName MissingLettersReadingRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Исчезающие буквы",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.MissingLettersReading
		};
		public readonly static Domain.Task.TaskGeneralName MissingLettersReadingEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Missing letters",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.MissingLettersReading
		};
		public readonly static Domain.Task.TaskGeneralName LettersPyramidsRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Буквенные пирамиды",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.LettersPyramids
		};
		public readonly static Domain.Task.TaskGeneralName LettersPyramidsEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Letters pyramids",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.LettersPyramids
		};
		public readonly static Domain.Task.TaskGeneralName ColourNamingRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Название цветов",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.ColourNaming
		};
		public readonly static Domain.Task.TaskGeneralName ColourNamingEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Colour naming",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.ColourNaming
		};
		public readonly static Domain.Task.TaskGeneralName PictureMemorizeRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Запоминание картинки",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.PictureMemorize
		};
		public readonly static Domain.Task.TaskGeneralName PictureMemorizeEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Picture memorize",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.PictureMemorize
		};
		public readonly static Domain.Task.TaskGeneralName PicturePairsRu = new()
		{
			LanguageId = Ru.Id,
			Title = "Пары картинок",
			Description = "Описание",
			ProgramName = Domain.Task.Enums.TaskName.PicturePairs
		};
		public readonly static Domain.Task.TaskGeneralName PicturePairsEng = new()
		{
			LanguageId = Eng.Id,
			Title = "Picture pairs",
			Description = "Description",
			ProgramName = Domain.Task.Enums.TaskName.PicturePairs
		};
		#endregion

		#region TrainingTasks
		public readonly static Domain.Task.Schulte SchulteWithNumbersRu1 = new()
		{
			Id = Guid.Parse("2B5195B7-EE67-4C80-8AD3-5FEB84B11A3D"),
			GeneralName = SchulteWithNumbersRu,
			Size = 3
		};
		public readonly static Domain.Task.Schulte SchulteWithNumbersRu2 = new()
		{
			Id = Guid.Parse("A499A2D4-6D63-4851-8557-82FBBCD3A967"),
			GeneralName = SchulteWithNumbersRu,
			Size = 4
		};
		public readonly static Domain.Task.Schulte SchulteWithNumbersEng1 = new()
		{
			Id = Guid.Parse("423F52E5-35BE-4360-B289-137F4B0CE32D"),
			GeneralName = SchulteWithNumbersEng,
			Size = 3
		};
		public readonly static Domain.Task.Schulte SchulteWithNumbersEng2 = new()
		{
			Id = Guid.Parse("1661BA60-2708-4BF6-9ED8-2C5B57CB70C6"),
			GeneralName = SchulteWithNumbersEng,
			Size = 4
		};
		public readonly static Domain.Task.TaskWithText MissingLettersReadingRu1 = new()
		{
			Id = Guid.Parse("B3E8A449-559F-4C3C-9EA4-4DA138A6CF02"),
			GeneralName = MissingLettersReadingRu,
			SecondsToComplete = 180,
			Text = TextRu
		};
		public readonly static Domain.Task.TaskWithText MissingLettersReadingEng1 = new()
		{
			Id = Guid.Parse("F40672F9-0422-4B62-86B8-5A6AFD399924"),
			GeneralName = MissingLettersReadingEng,
			SecondsToComplete = 240,
			Text = TextEng
		};
		public readonly static Domain.Task.LetterPyramids LetterPyramidsRu1 = new()
		{
			Id = Guid.Parse("62B67CFB-4CB9-4B93-9FCB-1A86DD66FC7D"),
			GeneralName = LettersPyramidsRu,
			Divider = '|',
			RepeatsCount = 4,
			Words = new() { WordRu1, WordRu2, WordRu3 }
		};
		public readonly static Domain.Task.LetterPyramids LetterPyramidsEng1 = new()
		{
			Id = Guid.Parse("A511FD0C-3D58-4238-8CFB-D5BEA49498FD"),
			GeneralName = LettersPyramidsEng,
			Divider = '|',
			RepeatsCount = 10,
			Words = new() { WordEng1, WordEng2, WordEng3 }
		};
		#endregion

		#region UsersStatistics
		public readonly static Domain.User.UserDailyStatistic UserAStatistic = new()
		{
			Id = 1,
			UserId = UserA.Id,
			Date = DateTime.Today,
			CompletedTasks = new()
			{
				new()
				{ 
					Time = TimeSpan.FromSeconds(25),
					CompletedTask = SchulteWithNumbersRu1
				},
				new()
				{
					Time = TimeSpan.FromSeconds(20),
					CompletedTask = SchulteWithNumbersRu1
				},
				new()
				{
					Time = TimeSpan.FromSeconds(30),
					CompletedTask = SchulteWithNumbersRu2
				},
				new()
				{
					CompletedTask = MissingLettersReadingRu1
				},
				new()
				{
					Time = TimeSpan.FromSeconds(45),
					CompletedTask = LetterPyramidsRu1
				}
			}
		};
		#endregion

		public static ApplicationDbContext Create()
		{
			DbContextOptions<ApplicationDbContext> options =
				new DbContextOptionsBuilder<ApplicationDbContext>()
					.UseInMemoryDatabase(Guid.NewGuid().ToString())
					.Options;

			ApplicationDbContext context = new(options);
			context.Database.EnsureCreated();
			#region AddLanguages
			context.Languages.AddRange(Ru, Eng);
			#endregion

			#region AddUsers
			context.Users.AddRange(
				UserA,
				UserB,
				new Domain.User.User
				{
					Id = UserIdForEdit,
					RegistrationDate = DateTime.Today,
					Email = "emailEdit",
					Login = "loginEdit",
					Password = AuthHelper.ComputePasswordHash("passwordEdit"),
					Avatar = string.Empty,
					FirstName = string.Empty,
					LastName = string.Empty,
					Broadcasting = true,
					UserInterfaceLanguageId = Ru.Id
				},
				new Domain.User.User
				{
					Id = UserIdForDelete,
					RegistrationDate = DateTime.Today,
					Email = "emailDelete",
					Login = "loginDelete",
					Password = AuthHelper.ComputePasswordHash("passwordDelete"),
					Avatar = string.Empty,
					FirstName = string.Empty,
					LastName = string.Empty,
					Broadcasting = true,
					UserInterfaceLanguageId = Ru.Id
				});
			#endregion

			#region AddTexts
			context.Texts.AddRange(TextRu, TextEng);
			#endregion

			#region AddWordsForLetterPyramid
			#endregion

			#region AddTasksGeneralNames
			context.TasksGeneralNames.AddRange(
				SchulteWithNumbersRu,
				SchulteWithNumbersEng,
				MissingLettersReadingRu,
				MissingLettersReadingEng,
				LettersPyramidsRu,
				LettersPyramidsEng,
				ColourNamingRu,
				ColourNamingEng,
				PictureMemorizeRu,
				PictureMemorizeEng,
				PicturePairsRu,
				PicturePairsEng);
			#endregion

			#region AddTrainingTasks
			context.Schultes.AddRange(
				SchulteWithNumbersRu1,
				SchulteWithNumbersRu2,
				SchulteWithNumbersEng1,
				SchulteWithNumbersEng2);
			context.TasksWithTexts.AddRange(MissingLettersReadingRu1, MissingLettersReadingEng1);
			context.LetterPyramids.AddRange(LetterPyramidsRu1, LetterPyramidsEng1);
			#endregion

			#region AddUsersStatistics
			context.UsersDailyStatistics.AddRange(UserAStatistic);
			#endregion
			context.SaveChanges();
			return context;
		}

		public static async System.Threading.Tasks.Task DestroyAsync(ApplicationDbContext context)
		{
			await context.Database.EnsureDeletedAsync();
			await context.DisposeAsync();
		}
	}
}
