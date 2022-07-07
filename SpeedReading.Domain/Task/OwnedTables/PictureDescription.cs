using Microsoft.EntityFrameworkCore;

namespace SpeedReading.Domain.Task.OwnedTables
{
	[Owned]
	public class PictureDescription
	{
		public Guid Id { get; set; }
		public Guid PicutreId { get; set; }
		public string Description { get; set; }
	}
}
