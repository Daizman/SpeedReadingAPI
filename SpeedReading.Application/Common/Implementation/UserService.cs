using Microsoft.EntityFrameworkCore;
using SpeedReading.Application.Common.Exceptions;
using SpeedReading.Application.Common.Interfaces;
using SpeedReading.Application.Dtos.User;
using SpeedReading.Domain.User;
using System.Security.Cryptography;
using System.Text;

namespace SpeedReading.Application.Common.Implementation
{
	public class UserService : BaseService, IUserService
	{
		private readonly SHA256 _SHA256;

		public UserService(IApplicationDbContext context) : base(context) 
			=> _SHA256 = SHA256.Create();

		public async Task<IEnumerable<UserDto>> GetUsersAsync()
		{
			return await _context.Users.Select(user => new UserDto
			{
				Id = user.Id,
				Login = user.Login,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Avatar = user.Avatar,
				RefreshTokens = user.RefreshTokens
			}).ToListAsync();
		}

		public async Task<UserDto> GetUserAsync(Guid id)
		{
			User user = await FindUserAsync(id);

			return user.AsDto();
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
				Password = ComputePasswordHash(dto.Password),
				Email = dto.Email,
				Avatar = dto.Avatar ?? string.Empty,
				FirstName = dto.FirstName ?? string.Empty,
				LastName = dto.LastName ?? string.Empty,
				RegistrationDate = DateTime.UtcNow
			};

			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();

			return user.Id;
		}

		public byte[] ComputePasswordHash(string password)
		{
			return _SHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
		}

		public async Task UpdateAsync(UpdateUserDto dto)
		{
			User user = await FindUserAsync(dto.Id);

			user.Login = dto.Login ?? user.Login;
			user.Password = string.IsNullOrWhiteSpace(dto.Password) ? user.Password : ComputePasswordHash(dto.Password);
			user.Email = dto.Email ?? user.Email;
			user.Avatar = dto.Avatar ?? user.Avatar;
			user.FirstName = dto.FirstName ?? user.FirstName;
			user.LastName = dto.LastName ?? user.LastName;
			
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

		private async Task<User> FindUserByLoginAsync(string login)
		{
			User? user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
			if (user is null)
			{
				throw new UserNotFoundException();
			}

			return user;
		}
	}
}
