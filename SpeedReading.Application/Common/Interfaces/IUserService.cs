using SpeedReading.Application.Dtos.User;

namespace SpeedReading.Application.Common.Interfaces
{
	public interface IUserService
	{
		Task<IEnumerable<UserDto>> GetUsersAsync();
		Task<UserDto> GetUserAsync(Guid id);
		Task<Guid> CreateAsync(CreateUserDto dto);
		Task UpdateAsync(UpdateUserDto dto);
		Task DeleteAsync(Guid id);
		byte[] ComputePasswordHash(string password);
	}
}
