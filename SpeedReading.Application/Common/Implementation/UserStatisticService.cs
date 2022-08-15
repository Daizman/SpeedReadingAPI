using SpeedReading.Application.Common.Helpers;
using SpeedReading.Application.Dtos.User;
using SpeedReading.Domain.Task;
using SpeedReading.Domain.Task.Enums;
using SpeedReading.Domain.User;
using SpeedReading.Domain.User.OwnedTables;

namespace SpeedReading.Application.Common.Implementation
{
	public class UserStatisticService : BaseService, IUserStatisticService
	{
		public UserStatisticService(IApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

		public async Task<UserDailyStatisticDto> GetUserDailyStatisticAsync(Guid userId, DateTime date)
		{
			await CheckUserExistenceAsync(userId);

			var statistic = await FindUserDailyStatisticAsync(userId, date);
			if (statistic is null)
			{
				return new(Guid.NewGuid(), userId, date, new(), new());
			}

			var (programTasks, additionalTasks) = await CalculateDailyStatisticAsync(statistic);

			return new(statistic.Id, statistic.UserId, statistic.Date, programTasks, additionalTasks);
		}

		public async Task AddUserTaskDailyStatisticAsync(AddUserTaskDailyStatisticDto dailyStatistic)
		{
			await CheckUserExistenceAsync(dailyStatistic.UserId);

			var statistic = await FindUserDailyStatisticAsync(dailyStatistic.UserId, dailyStatistic.Date);
			statistic ??= await CreateUserDailyStatisticAsync(dailyStatistic);

			UserTaskStatistic currentTaskStatistic = new()
			{
				CompletedTask = await FindTaskAsync(dailyStatistic.TaskId, dailyStatistic.TaskName),
				Time = dailyStatistic.Time
			};
			statistic.CompletedTasks.Add(currentTaskStatistic);
			await _context.SaveChangesAsync();
		}

		private async Task<UserDailyStatistic> CreateUserDailyStatisticAsync(AddUserTaskDailyStatisticDto dailyStatistic)
		{
			List<UserTaskStatistic> dailyCompletedTask = new();

			UserDailyStatistic statistic = new()
			{
				UserId = dailyStatistic.UserId,
				Date = dailyStatistic.Date,
				CompletedTasks = dailyCompletedTask
			};

			await _context.UsersDailyStatistics.AddAsync(statistic);
			await _context.SaveChangesAsync();

			return statistic;
		}

		private async Task CheckUserExistenceAsync(Guid userId)
		{
			if (!await _context.Users.AnyAsync(u => u.Id == userId))
			{
				throw new UserNotFoundException();
			}
		}

		private async Task<UserDailyStatistic?> FindUserDailyStatisticAsync(Guid userId, DateTime date)
		{
			return await _context.UsersDailyStatistics.FirstOrDefaultAsync(uds => uds.UserId == userId && uds.Date.Date == date.Date);
		}

		private async Task<(List<UserDailyTaskStatisticDto>, List<UserDailyTaskStatisticDto>)> CalculateDailyStatisticAsync(UserDailyStatistic statistic)
		{
			var completedTasks = statistic.CompletedTasks.GroupBy(ct => ct.CompletedTask.GeneralName.ProgramName);

			List<UserDailyTaskStatisticDto> programTasks = new();
			List<UserDailyTaskStatisticDto> additionalTasks = new();
			foreach (var task in completedTasks)
			{
				var generalTaskInfo = task.First();
				var record = await FindUserRecrodTimeInTaskAsync(statistic.UserId, generalTaskInfo.CompletedTask.GeneralName.ProgramName);

				UserDailyTaskStatisticDto stat = new(
						generalTaskInfo.CompletedTask.Id,
						generalTaskInfo.CompletedTask.GeneralName.Title,
						TaskHelper.TaskSkillToString(generalTaskInfo.CompletedTask.Skill),
						task.Count(),
						generalTaskInfo.Time,
						record);

				if (programTasks.Count < 10)
				{
					programTasks.Add(stat);
				}
				else
				{
					additionalTasks.Add(stat);
				}
			}
			return (programTasks, additionalTasks);
		}

		private async Task<TimeSpan?> FindUserRecrodTimeInTaskAsync(Guid userId, TaskName taskName)
		{
			return await _context.UsersDailyStatistics.Where(uds => uds.UserId == userId)
												      .SelectMany(uds => uds.CompletedTasks)
													  .Where(ct => ct.CompletedTask.GeneralName.ProgramName == taskName && ct.Time.HasValue)
												      .OrderByDescending(ct => ct.Time)
													  .Select(ct => ct.Time)
													  .FirstOrDefaultAsync();
		}

		private async Task<TrainingTask> FindTaskAsync(Guid taskId, TaskName taskName)
		{
			var generalInfo = await _context.TasksGeneralNames.FirstOrDefaultAsync(tgn => tgn.ProgramName == taskName);
			if (generalInfo is null)
			{
				throw new TaskNotFoundException();
			}

			TrainingTask? task = taskName switch
			{
				TaskName.SchulteWithNumbers | TaskName.SchulteWithLetters 
				=> await _context.Schultes.FirstOrDefaultAsync(s => s.Id == taskId),
				
				TaskName.PictureMemorize => await _context.PictureMemorizes.FirstOrDefaultAsync(pm => pm.Id == taskId),

				TaskName.LettersPyramids => await _context.LetterPyramids.FirstOrDefaultAsync(lp => lp.Id == taskId),

				TaskName.ColourNaming => await _context.ColourNamings.FirstOrDefaultAsync(cn => cn.Id == taskId),
				TaskName.PicturePairs => await _context.PicturePairs.FirstOrDefaultAsync(pp => pp.Id == taskId),

				_ => await _context.TasksWithTexts.FirstOrDefaultAsync(twt => twt.Id == taskId)
			};

			if(task is null)
			{
				throw new TaskNotFoundException();
			}

			return task;
		}
	}
}
