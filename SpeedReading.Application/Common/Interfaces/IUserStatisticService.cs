using SpeedReading.Application.Dtos.User;

namespace SpeedReading.Application.Common.Interfaces
{
	public interface IUserStatisticService
	{
		Task<UserDailyStatisticDto> GetUserDailyStatistic(Guid userId, DateTime date);
		Task AddUserTaskDailyStatistic(AddUserTaskDailyStatisticDto dailyStatistic);
	}
}
