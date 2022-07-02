using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace SpeedReading.Domain.Task.OwnedTables
{
	[Owned]
	public class HexColour
	{
		[JsonIgnore]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Hex { get; set; }
	}
}
