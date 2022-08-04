using SpeedReading.Domain.Auth;
using System.Text.Json.Serialization;

namespace SpeedReading.Application.Dtos.User
{
	public record CreateUserDto(
		string Login,
		string Password,
		string Email,
		string? Avatar,
		string? FirstName,
		string? LastName,
		bool Broadcasting);

	public record UpdateUserDto(
		Guid Id,
		string? Login,
		string? Password,
		string? Email,
		string? Avatar,
		string? FirstName,
		string? LastName,
		bool? Broadcasting);

	public record UserDto : IMapWith<Domain.User.User>
	{
		public Guid Id { get; init; }
		public string Login { get; init; }
		public string Email { get; init; }
		public string Avatar { get; init; }
		public string FirstName { get; init; }
		public string LastName { get; init; }

		public bool Broadcasting { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Domain.User.User, UserDto>()
				.ForMember(userDto => userDto.Id, opt => opt.MapFrom(user => user.Id))
				.ForMember(userDto => userDto.Login, opt => opt.MapFrom(user => user.Login))
				.ForMember(userDto => userDto.Email, opt => opt.MapFrom(user => user.Email))
				.ForMember(userDto => userDto.Avatar, opt => opt.MapFrom(user => user.Avatar))
				.ForMember(userDto => userDto.FirstName, opt => opt.MapFrom(user => user.FirstName))
				.ForMember(userDto => userDto.LastName, opt => opt.MapFrom(user => user.LastName))
				.ForMember(userDto => userDto.Broadcasting, opt => opt.MapFrom(user => user.Broadcasting))
				.ForMember(userDto => userDto.RefreshTokens, opt => opt.MapFrom(user => user.RefreshTokens));
		}

		[JsonIgnore]
		public List<RefreshToken> RefreshTokens { get; set; }
	}

	public record UserLookupDto : IMapWith<Domain.User.User>
	{
		public Guid Id { get; init; }
		public string Login { get; init; }
		public string Email { get; init; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Domain.User.User, UserLookupDto>()
				.ForMember(userDto => userDto.Id, opt => opt.MapFrom(user => user.Id))
				.ForMember(userDto => userDto.Login, opt => opt.MapFrom(user => user.Login))
				.ForMember(userDto => userDto.Email, opt => opt.MapFrom(user => user.Email));
		}
	}

	public record UserListDto(IList<UserLookupDto> Users);
}
