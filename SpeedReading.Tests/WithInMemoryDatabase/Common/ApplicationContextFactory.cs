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

		#endregion

		#region UsersStatistics
		public readonly static Domain.User.UserDailyStatistic UserAStatistic = new()
		{
			Id = Guid.Parse("609FA5B5-1150-494C-B2A1-0C87F19E513A"),
			UserId = UserA.Id,
			Date = DateTime.Today,
			CompletedTasks = new()
			{
				new()
				{ 
					Time = TimeSpan.FromSeconds(25),
					CompletedTask = SchulteWithNumbersRu1
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
			context.Languages.AddRangeAsync(
				Ru,
				Eng);

			#region AddUsers
			context.Users.AddRangeAsync(
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
