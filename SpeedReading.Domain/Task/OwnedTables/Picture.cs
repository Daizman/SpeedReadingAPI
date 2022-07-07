using Microsoft.EntityFrameworkCore;

namespace SpeedReading.Domain.Task.OwnedTables
{
	[Owned]
	public class Picture
	{
		public Guid Id { get; set; }
		// base64 format
		public string Content { get; set; }
	}
}
