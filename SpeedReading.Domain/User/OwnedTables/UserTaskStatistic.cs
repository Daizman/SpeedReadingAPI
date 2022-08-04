using Microsoft.EntityFrameworkCore;
using SpeedReading.Domain.Task;

namespace SpeedReading.Domain.User.OwnedTables
{
	[Owned]
	public class UserTaskStatistic
	{
		public TrainingTask CompletedTask { get; set; }
		public TimeSpan? Time { get; set; }
	}
}
