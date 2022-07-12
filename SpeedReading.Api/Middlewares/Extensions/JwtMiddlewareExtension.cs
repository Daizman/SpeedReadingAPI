namespace SpeedReading.Api.Middlewares.Extensions
{
	public static class JwtMiddlewareExtension
	{
		public static IApplicationBuilder UseJwt(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<JwtMiddleware>();
		}
	}
}
