using SpeedReading.Application.Dtos.User;

namespace SpeedReading.Application.Common.Interfaces
{
	public interface IUserStatisticService
	{
		Task<UserDailyStatisticDto> GetUserDailyStatisticAsync(Guid userId, DateTime date);
		Task AddUserTaskDailyStatisticAsync(AddUserTaskDailyStatisticDto dailyStatistic);
	}
}
