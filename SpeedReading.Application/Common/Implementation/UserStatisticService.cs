using SpeedReading.Application.Common.Helpers;
using SpeedReading.Application.Dtos.User;
using SpeedReading.Domain.Task;
using SpeedReading.Domain.Task.Enums;
using SpeedReading.Domain.User;

namespace SpeedReading.Application.Common.Implementation
{
	public class UserStatisticService : BaseService, IUserStatisticService
	{
		public UserStatisticService(IApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

		public async Task<UserDailyStatisticDto> GetUserDailyStatistic(Guid userId, DateTime date)
		{
			await CheckUserExistence(userId);

			var statistic = await FindUserDailyStatistic(userId, date);
			if (statistic is null)
			{
				return new(Guid.NewGuid(), userId, date, new(), new());
			}

			var (programTasks, additionalTasks) = await CalculateDailyStatistic(statistic);

			return new(statistic.Id, statistic.UserId, statistic.Date, programTasks, additionalTasks);
		}

		public async Task AddUserTaskDailyStatistic(AddUserTaskDailyStatisticDto dailyStatistic)
		{
			await CheckUserExistence(dailyStatistic.UserId);

			var statistic = await FindUserDailyStatistic(dailyStatistic.UserId, dailyStatistic.Date);

			if (statistic is null)
			{
				statistic = new()
				{
					UserId = dailyStatistic.UserId,
					Date = dailyStatistic.Date,
					CompletedTasks = new() 
					{ 
						new()
						{
							CompletedTask = await FindTask(dailyStatistic.TaskId, dailyStatistic.TaskName),
							Time = dailyStatistic.Time
						}
					}
				};
			}
		}

		private async Task CheckUserExistence(Guid userId)
		{
			if (!await _context.Users.AnyAsync(u => u.Id == userId))
			{
				throw new UserNotFoundException();
			}
		}

		private async Task<UserDailyStatistic?> FindUserDailyStatistic(Guid userId, DateTime date)
		{
			return await _context.UsersDailyStatistics.FirstOrDefaultAsync(uds => uds.UserId == userId && uds.Date.Date == date.Date);
		}


		private async Task<(List<UserDailyTaskStatisticDto>, List<UserDailyTaskStatisticDto>)> CalculateDailyStatistic(UserDailyStatistic statistic)
		{
			var completedTasks = statistic.CompletedTasks.GroupBy(ct => ct.CompletedTask.GeneralName.ProgramName);

			List<UserDailyTaskStatisticDto> programTasks = new();
			List<UserDailyTaskStatisticDto> additionalTasks = new();
			foreach (var task in completedTasks)
			{
				var generalTaskInfo = task.First();
				var record = await FindUserRecrodTimeInTask(statistic.UserId, generalTaskInfo.CompletedTask.GeneralName.ProgramName);

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

		private async Task<TimeSpan?> FindUserRecrodTimeInTask(Guid userId, TaskName taskName)
		{
			return await _context.UsersDailyStatistics.Where(uds => uds.UserId == userId)
												      .SelectMany(uds => uds.CompletedTasks)
													  .Where(ct => ct.CompletedTask.GeneralName.ProgramName == taskName && ct.Time.HasValue)
												      .OrderByDescending(ct => ct.Time)
													  .Select(ct => ct.Time)
													  .FirstOrDefaultAsync();
		}

		private async Task<TrainingTask> FindTask(Guid taskId, TaskName taskName)
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
