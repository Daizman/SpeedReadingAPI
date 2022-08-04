using Microsoft.AspNetCore.Mvc;
using SpeedReading.Api.Attributes.Auth;
using SpeedReading.Application.Common.Interfaces;
using SpeedReading.Application.Dtos;

namespace SpeedReading.Api.Controllers
{
	public class AuthController : CommonController
	{
		private readonly IAuthService _service;

		public AuthController(IAuthService service)
		{
			_service = service;
		}

		/// <summary>
		/// Authanticate user by login and password
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// POST /auth
		/// {
		///		login: "login1",
		///		password: "password1"
		/// }
		/// </remarks>
		/// <param name="request">UserAuthRequestDto object</param>
		/// <returns>Returns UserAuthResponseDto</returns>
		/// <response code="200">Success</response>
		[AllowAnonymous]
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<UserAuthResponseDto>> AuthenticateAsync(UserAuthRequestDto request)
		{
			var response = await _service.AuthenticateAsync(request, GetIpAddress());
			SetTokenCookie(response.RefreshToken);
			return Ok(response);
		}

		private string GetIpAddress()
		{
			if (Request.Headers.ContainsKey("X-Forwarded-For"))
			{
				return Request.Headers["X-Forwarded-For"];
			}
			return HttpContext.Connection.RemoteIpAddress?.MapToIPv4()?.ToString() ?? string.Empty;
		}

		private void SetTokenCookie(string token)
		{
			CookieOptions options = new()
			{
				HttpOnly = true,
				Expires = DateTime.Now.AddDays(7)
			};
			Response.Cookies.Append("refreshToken", token, options);
		}

		/// <summary>
		/// Refresh user token
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// POST /auth
		/// </remarks>
		/// <returns>Returns UserAuthResponseDto</returns>
		/// <response code="200">Success</response>
		/// <response code="400">Token not found</response>
		[AllowAnonymous]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<UserAuthResponseDto>> RefreshTokenAsync()
		{
			var refreshToken = Request.Cookies["refreshToken"];
			if (string.IsNullOrEmpty(refreshToken))
			{
				return BadRequest(new { message = "Token is required" });
			}
			var response = await _service.RefreshTokenAsync(refreshToken, GetIpAddress());
			SetTokenCookie(response.RefreshToken);
			return Ok(response);
		}
	}
}
