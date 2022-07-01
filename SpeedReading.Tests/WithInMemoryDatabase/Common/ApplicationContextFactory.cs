﻿using Microsoft.EntityFrameworkCore;
using SpeedReading.Persistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedReading.Tests.WithInMemoryDatabase.Common
{
	public class ApplicationContextFactory
	{
		#region Users
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
				new Domain.User.User
				{ 
					Id = Guid.Parse("2CAAA47D-2B34-4285-A26E-E00DB43A7BD9"),
					RegistrationDate = DateTime.Today
				},
				new Domain.User.User
				{
					Id = Guid.Parse("7037CC33-7435-4316-B3AF-4FA3907CCEB3"),
					RegistrationDate = DateTime.Today
				},
				new Domain.User.User
				{
					Id = UserIdForEdit,
					RegistrationDate = DateTime.Today
				},
				new Domain.User.User
				{
					Id = UserIdForDelete,
					RegistrationDate = DateTime.Today
				});
			#endregion

			context.SaveChanges();
			return context;
		}

		public static async Task Destroy(ApplicationDbContext context)
		{
			await context.Database.EnsureDeletedAsync();
			await context.DisposeAsync();
		}
	}
}