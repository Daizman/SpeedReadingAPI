using Microsoft.AspNetCore.Mvc;
using SpeedReading.Api.Attributes.Auth;
using SpeedReading.Application.Common.Interfaces;
using SpeedReading.Application.Dtos.User;

namespace SpeedReading.Api.Controllers
{
	public class UserStatisticController : CommonController
	{
		private readonly IUserStatisticService _service;

		public UserStatisticController(IUserStatisticService service)
		{
			_service = service;
		}

		/// <summary>
		/// Gets user statistic for day
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// GET /userStatistic/daily/98BC106D-91B0-48FC-9091-1806B9ED6442?date=yyyy-mm-dd
		/// </remarks>
		/// <param name="userId">User Guid</param>
		/// <param name="date">Date</param>
		/// <returns>Returns UserDailyStatisticDto</returns>
		/// <response code="200">Success</response>
		[AllowAnonymous]
		[HttpGet("daily/{userId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<UserDailyStatisticDto>> GetAsync(Guid userId, DateTime date)
		{
			var statistic = await _service.GetUserDailyStatisticAsync(userId, date);
			return Ok(statistic);
		}
	}
}
