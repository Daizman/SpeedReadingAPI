namespace SpeedReading.Application.Common.Models
{
	public class AppSettings
	{
		public string Secret { get; set; }

		// время жизни рефреш токена в днях,
		// не используемые токены автоматически удаляются по истечению этого времени
		public int RefreshTokenTTL { get; set; }
	}
}
