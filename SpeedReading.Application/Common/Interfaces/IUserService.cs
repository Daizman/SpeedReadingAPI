﻿using SpeedReading.Application.Dtos.User;

namespace SpeedReading.Application.Common.Interfaces
{
	public interface IUserService
	{
		Task<UserListDto> GetUsersAsync();
		Task<UserDto> GetUserAsync(Guid id);
		Task<Guid> CreateAsync(CreateUserDto dto);
		Task UpdateAsync(UpdateUserDto dto);
		Task DeleteAsync(Guid id);
	}
}
