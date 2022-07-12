using SpeedReading.Application.Common.Interfaces;

namespace SpeedReading.Api.Middlewares
{
	public class JwtMiddleware
	{
		private readonly RequestDelegate _next;

		public JwtMiddleware(RequestDelegate next) => _next = next;

		public async Task InvokeAsync(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
		{
			var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split().Last();
			var userId = await jwtUtils.ValidateJwtTokenAsync(token);
			if (userId != Guid.Empty)
			{
				context.Items.Add("UserDto", userService.GetUserAsync(userId));
			}

			await _next(context);
		}
	}
}
