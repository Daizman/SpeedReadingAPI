using Microsoft.AspNetCore.Mvc;
using SpeedReading.Api.Attributes.Auth;

namespace SpeedReading.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class CommonController : ControllerBase { }
}
