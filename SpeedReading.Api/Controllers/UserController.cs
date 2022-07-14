using Microsoft.AspNetCore.Mvc;
using SpeedReading.Api.Attributes.Auth;
using SpeedReading.Application.Common.Interfaces;
using SpeedReading.Application.Dtos.User;
using SpeedReading.Domain.Auth;

namespace SpeedReading.Api.Controllers
{
	public class UserController : CommonController
	{
		private readonly IUserService _service;
		public UserController(IUserService service)
		{
			_service = service;
		}

		/// <summary>
		/// Gets user by identifier
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// GET /user/get/98BC106D-91B0-48FC-9091-1806B9ED6442
		/// </remarks>
		/// <param name="userId">User Guid</param>
		/// <returns>Returns UserDto</returns>
		/// <response code="200">Success</response>
		[AllowAnonymous]
		[HttpGet("{userId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<UserDto>> GetAsync(Guid userId)
		{
			var user = await _service.GetUserAsync(userId);
			return Ok(user);
		}

		/// <summary>
		/// Gets users list
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// GET /user/get
		/// </remarks>
		/// <returns>Returns UserListDto</returns>
		/// <response code="200">Success</response>
		/// <response code="401">User unauthorized</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<UserListDto>> GetAsync()
		{
			var users = await _service.GetUsersAsync();
			return Ok(users);
		}

		/// <summary>
		/// Create user
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// POST /user/createAsync
		/// {
		///		login: "login1",
		///		password: "password1",
		///		email: "email1",
		///		avatar: "base64image" | null,
		///		firstName: "firstName" | null,
		///		lastName: "lastName" | null
		/// }
		/// </remarks>
		/// <param name="dto">CreateUserDto object</param>
		/// <returns>User Guid</returns>
		/// <response code="201">Success</response>
		[AllowAnonymous]
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<ActionResult<Guid>> CreateAsync(CreateUserDto dto)
		{
			var response = await _service.CreateAsync(dto);
			return Ok(response);
		}

		/// <summary>
		/// Update user
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// PUT /user/updateAsync
		/// {
		///		id: "B1D7D546-788C-4D3E-9057-C4AFE9C9A8C3",
		///		login: "login1" | null,
		///		password: "password1" | null,
		///		email: "email1" | null,
		///		avatar: "base64image" | null,
		///		firstName: "firstName" | null,
		///		lastName: "lastName" | null
		/// }
		/// </remarks>
		/// <param name="dto"></param>
		/// <returns>NoContent</returns>
		/// <response code="204">Success</response>
		/// <response code="401">User unauthorized</response>
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> UpdateAsync(UpdateUserDto dto)
		{
			await _service.UpdateAsync(dto);
			return NoContent();
		}

		/// <summary>
		/// Delete user
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// DELETE /user/deleteAsync/2EF66775-6564-4F8F-9153-4E475E68A716
		/// </remarks>
		/// <param name="userId">User Guid</param>
		/// <returns>NoContent</returns>
		/// <response code="204">Success</response>
		/// <response code="401">User unauthorized</response>
		[HttpDelete("{userId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> DeleteAsync(Guid userId)
		{
			await _service.DeleteAsync(userId);
			return NoContent();
		}

		/// <summary>
		/// Gets user refresh tokens
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// GET /user/getRefreshTokensAsync/D14BF7D0-4442-4276-AA97-C5941CCC17D5
		/// </remarks>
		/// <param name="userId">User Guid</param>
		/// <returns>RefreshToken List</returns>
		/// <response code="200">Success</response>
		/// <response code="401">User unauthorized</response>
		[HttpGet("{userId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<IEnumerable<RefreshToken>>> GetRefreshTokensAsync(Guid userId)
		{
			var user = await _service.GetUserAsync(userId);
			return Ok(user.RefreshTokens);
		}
	}
}
