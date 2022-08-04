using Microsoft.EntityFrameworkCore;
using SpeedReading.Application.Common.Helpers;
using SpeedReading.Persistent;

namespace SpeedReading.Tests.WithInMemoryDatabase.Common
{
	public class ApplicationContextFactory
	{
		#region Users
		public readonly static Domain.User.User UserA = new()
		{
			Id = Guid.Parse("2CAAA47D-2B34-4285-A26E-E00DB43A7BD9"),
			RegistrationDate = DateTime.Today,
			Email = "email1",
			Login = "login1",
			Password = AuthHelper.ComputePasswordHash("password1"),
			Avatar = "base64(dsadsald;)",
			FirstName = "Alex",
			LastName = "Tom",
			Broadcasting = false
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
			Broadcasting = false
		};

		public readonly static Guid UserIdForEdit = Guid.NewGuid();
		public readonly static Guid UserIdForDelete = Guid.NewGuid();
		#endregion

		public static ApplicationDbContext Create()
		{
			DbContextOptions<ApplicationDbContext> options =
				new DbContextOptionsBuilder<ApplicationDbContext>()
					.UseInMemoryDatabase(Guid.NewGuid().ToString())
					.Options;

			ApplicationDbContext context = new(options);
			context.Database.EnsureCreated();

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
					Broadcasting = true
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
					Broadcasting = true
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
