using SpeedReading.Application.Dtos.User;

namespace SpeedReading.Application.Common.Interfaces
{
	public interface IUserService
	{
		Task<UserDto> Get(Guid id);
		Task<Guid> Create(CreateUserDto dto);
		Task Update(UpdateUserDto dto);
		Task Delete(Guid id);
	}
}
