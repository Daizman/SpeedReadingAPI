using SpeedReading.Domain.Task.Enums;
using System.ComponentModel.DataAnnotations;

namespace SpeedReading.Domain.Task
{
	public class TaskGeneralName
	{
		[Key]
		public TaskName ProgramName { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
	}
}
