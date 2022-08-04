using Microsoft.AspNetCore.Mvc;
using SpeedReading.Api.Attributes.Auth;

namespace SpeedReading.Api.Controllers
{
	[ApiVersionNeutral]
	[Authorize]
	[ApiController]
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class CommonController : ControllerBase { }
}
