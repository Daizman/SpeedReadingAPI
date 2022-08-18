using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SpeedReading.Application.Common.Exceptions;
using SpeedReading.Application.Common.Implementation;
using SpeedReading.Application.Dtos.User;
using SpeedReading.Tests.WithInMemoryDatabase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeedReading.Tests.WithInMemoryDatabase.User
{
	[Collection("QueryCollection")]
	public class UserStatisticTests
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public UserStatisticTests(QueryTestFixture testFixture) =>
			(_context, _mapper) = (testFixture.Context, testFixture.Mapper);

		#region ReadOperations
		[Fact]
		public async System.Threading.Tasks.Task GetUserDailyStatisticAsync_WithUnexistingUser_ThrowsUserNotFound()
		{
			// Arrange
			var statisticService = new UserStatisticService(_context, _mapper);
			// Act
			var act = async () => await statisticService.GetUserDailyStatisticAsync(Guid.NewGuid(), DateTime.Now);
			// Assert
			await Assert.ThrowsAsync<UserNotFoundException>(act);
		}

		[Fact]
		public async System.Threading.Tasks.Task GetUserDailyStatisticAsync_WithUnexistingStatistic_ReturnsDefault()
		{
			// Arrange
			var statisticService = new UserStatisticService(_context, _mapper);
			var date = DateTime.Today.AddDays(-1);
			var userId = ApplicationContextFactory.UserA.Id;
			// Act
			var result = await statisticService.GetUserDailyStatisticAsync(userId, date);
			// Assert
			result.Should().BeOfType<UserDailyStatisticDto>();
			result.Id.Should().Be(default);
			result.UserId.Should().Be(userId);
			result.Date.Should().BeCloseTo(date, TimeSpan.FromSeconds(3));
			result.CompletedTasks.Should().BeEmpty();
			result.AdditionalTasks.Should().BeEmpty();
		}

		[Fact]
		public async System.Threading.Tasks.Task GetUserDailyStatisticAsync_WithExistingStatisticOnlyProgram_ReturnsStatistic()
		{
			// Arrange
			var statisticService = new UserStatisticService(_context, _mapper);
			var date = DateTime.Today;
			var userId = ApplicationContextFactory.UserA.Id;
			var recordTask = ApplicationContextFactory.SchulteWithNumbersRu1.Id;
			// Act
			var result = await statisticService.GetUserDailyStatisticAsync(userId, date);
			// Assert
			result.Should().BeOfType<UserDailyStatisticDto>();
			result.Id.Should().Be(1);
			result.UserId.Should().Be(userId);
			result.Date.Should().BeCloseTo(date, TimeSpan.FromSeconds(3));
			result.CompletedTasks.Should().NotBeEmpty();
			result.CompletedTasks.Should().HaveCount(4);
			result.CompletedTasks.Find(ct => ct.TaskId == recordTask).Should().NotBeNull();
			result.CompletedTasks.Find(ct => ct.TaskId == recordTask)?.CompleteCount.Should().Be(2);
			result.CompletedTasks.Find(ct => ct.TaskId == recordTask)?.RecordTime.Should().Be(TimeSpan.FromSeconds(20));
			result.AdditionalTasks.Should().BeEmpty();
		}
		#endregion

		#region AddOperations
		[Fact]
		public async System.Threading.Tasks.Task AddUserTaskDailyStatisticAsync_WithUnexistingUser_ThrowsUserNotFound()
		{
			// Arrange
			var statisticService = new UserStatisticService(_context, _mapper);
			var statistic = new AddUserTaskDailyStatisticDto(Guid.NewGuid(), Guid.NewGuid(), Domain.Task.Enums.TaskName.LettersPyramids, DateTime.Now, null);
			// Act
			var act = async () => await statisticService.AddUserTaskDailyStatisticAsync(statistic);
			// Assert
			await Assert.ThrowsAsync<UserNotFoundException>(act);
		}

		[Fact]
		public async System.Threading.Tasks.Task AddUserTaskDailyStatisticAsync_WithUnexistingTask_ThrowsTaskNotFound()
		{
			// Arrange
			var statisticService = new UserStatisticService(_context, _mapper);
			var userId = ApplicationContextFactory.UserA.Id;
			var statistic = new AddUserTaskDailyStatisticDto(userId, Guid.NewGuid(), Domain.Task.Enums.TaskName.LettersPyramids, DateTime.Now, null);
			// Act
			var act = async () => await statisticService.AddUserTaskDailyStatisticAsync(statistic);
			// Assert
			await Assert.ThrowsAsync<TaskNotFoundException>(act);
		}

		[Fact]
		public async System.Threading.Tasks.Task AddUserTaskDailyStatisticAsync_WithCorrectData_ReturnsTask()
		{
			// Arrange
			var statisticService = new UserStatisticService(_context, _mapper);
			var userId = ApplicationContextFactory.UserA.Id;
			var task = ApplicationContextFactory.MissingLettersReadingRu1;
			var date = DateTime.Today.AddDays(1);
			var statistic = new AddUserTaskDailyStatisticDto(userId, task.Id, task.GeneralName.ProgramName, date, null);
			// Act
			await statisticService.AddUserTaskDailyStatisticAsync(statistic);
			var result = await _context.UsersDailyStatistics.FirstOrDefaultAsync(stat =>
					stat.UserId == userId
					&& stat.Date.Equals(date));
			// Assert

			Assert.NotNull(result);
			result?.CompletedTasks.Should().HaveCount(1);
			result?.CompletedTasks?[0].Time.Should().BeNull();
			result?.CompletedTasks?[0].CompletedTask.Should().BeEquivalentTo(task);
		}
		#endregion
	}
}
