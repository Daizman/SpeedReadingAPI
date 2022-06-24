namespace SpeedReading.Domain.User
{
	public class UserText : Text
	{
		public Guid UserId { get; set; }
		public DateTime UploadDate { get; set; }
	}
}
