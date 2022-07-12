using SpeedReading.Application.Common.Exceptions;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace SpeedReading.Api.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception e)
			{
				await HandleException(context, e);
			}
		}

		private Task HandleException(HttpContext context, Exception exception)
		{
			var code = HttpStatusCode.InternalServerError;
			var result = string.Empty;
			switch (exception)
			{
				case IncorrectPasswordException:
				case InvalidTokenException:
				case UserAlreadyExistsException:
					code = HttpStatusCode.Conflict;
					break;
				case UserNotFoundException:
					code = HttpStatusCode.NotFound;
					break;
			}

			context.Response.ContentType = MediaTypeNames.Application.Json;
			context.Response.StatusCode = (int)code;
			if (result == string.Empty)
			{
				result = JsonSerializer.Serialize(new { error = exception.Message });
			}

			return context.Response.WriteAsync(result);
		}
	}
}
