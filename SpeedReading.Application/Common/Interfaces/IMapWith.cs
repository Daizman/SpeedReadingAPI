using AutoMapper;

namespace SpeedReading.Application.Common.Interfaces
{
	public interface IMapWith<T>
	{
		void Mapping(Profile profile)
			=> profile.CreateMap(typeof(T), GetType());
	}
}
