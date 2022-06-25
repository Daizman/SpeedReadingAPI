using SpeedReading.Application.Common.Interfaces;

namespace SpeedReading.Application.Common.Implementation
{
	public abstract class BaseService
	{
		protected readonly IApplicationDbContext _context;

		public BaseService(IApplicationDbContext context) => _context = context;
	}
}
