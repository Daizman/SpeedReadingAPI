using AutoMapper;
using SpeedReading.Application.Common.Interfaces;
using System.Reflection;

namespace SpeedReading.Application.Common.Mapping
{
	public class AssemblyMappingProfile : Profile
	{
		public AssemblyMappingProfile(Assembly assembly) => ApplyMappingFromAssembly(assembly);

		private void ApplyMappingFromAssembly(Assembly assembly)
		{
			var types = assembly.GetExportedTypes()
								.Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
								.ToList();

			foreach (var type in types)
			{
				var instance = Activator.CreateInstance(type);
				var methodInfo = type.GetMethod("Mapping");
				methodInfo?.Invoke(instance, new object[] { this });
			}
		}
	}
}
