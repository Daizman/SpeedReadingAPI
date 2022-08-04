namespace SpeedReading.Application.Common.Implementation
{
	public abstract class BaseService
	{
		protected readonly IApplicationDbContext _context;
		protected readonly IMapper _mapper;

		public BaseService(IApplicationDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);
	}
}
