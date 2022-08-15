using AutoMapper.QueryableExtensions;
using SpeedReading.Application.Common.Helpers;
using SpeedReading.Application.Dtos.User;
using SpeedReading.Domain.User;

namespace SpeedReading.Application.Common.Implementation
{
	public class UserService : BaseService, IUserService
	{

		public UserService(IApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

		public async Task<UserListDto> GetUsersAsync()
		{
			List<UserLookupDto> users = 
				await _context.Users.ProjectTo<UserLookupDto>(_mapper.ConfigurationProvider).ToListAsync();
			return new UserListDto(users);
		}

		public async Task<UserDto> GetUserAsync(Guid id)
		{
			User user = await FindUserAsync(id);
			return _mapper.Map<UserDto>(user);
		}

		public async Task<Guid> CreateAsync(CreateUserDto dto)
		{
			if (await _context.Users.AnyAsync(u => u.Login == dto.Login || u.Email == dto.Email))
			{
				throw new UserAlreadyExistsException(dto.Login, dto.Email);
			}

			User user = new()
			{
				Id = Guid.NewGuid(),
				Login = dto.Login,
				Password = AuthHelper.ComputePasswordHash(dto.Password),
				Email = dto.Email,
				Avatar = dto.Avatar ?? string.Empty,
				FirstName = dto.FirstName ?? string.Empty,
				LastName = dto.LastName ?? string.Empty,
				RegistrationDate = DateTime.UtcNow,
				Broadcasting = dto.Broadcasting,
				UserInterfaceLanguageId = dto.LanguageId
			};

			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();

			return user.Id;
		}

		public async Task UpdateAsync(UpdateUserDto dto)
		{
			User user = await FindUserAsync(dto.Id);

			user.Login = dto.Login ?? user.Login;
			user.Password = string.IsNullOrWhiteSpace(dto.Password) ? user.Password : AuthHelper.ComputePasswordHash(dto.Password);
			user.Email = dto.Email ?? user.Email;
			user.Avatar = dto.Avatar ?? user.Avatar;
			user.FirstName = dto.FirstName ?? user.FirstName;
			user.LastName = dto.LastName ?? user.LastName;
			user.Broadcasting = dto.Broadcasting ?? user.Broadcasting;
			user.UserInterfaceLanguageId = dto.LanguageId ?? user.UserInterfaceLanguageId;
			
			await _context.SaveChangesAsync();
		}

		private async Task<User> FindUserAsync(Guid userId)
		{
			User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
			if (user is null)
			{
				throw new UserNotFoundException();
			}

			return user;
		}

		public async Task DeleteAsync(Guid id)
		{
			User user = await FindUserAsync(id);

			_context.Users.Remove(user);

			await _context.SaveChangesAsync();
		}
	}
}
