using AutoMapper;
using AccrediGo.Application.Interfaces;
using System.Reflection;

namespace AccrediGo.Application.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Automatically discover and register all IMapTo implementations
            RegisterMapToImplementations();
        }

        private void RegisterMapToImplementations()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                var mapToInterfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>));

                foreach (var mapToInterface in mapToInterfaces)
                {
                    var entityType = mapToInterface.GetGenericArguments()[0];
                    
                    // Check if the type implements IMapTo and has a Mapping method
                    if (typeof(IMapTo<>).MakeGenericType(entityType).IsAssignableFrom(type))
                    {
                        // Create an instance and call the Mapping method
                        try
                        {
                            var instance = Activator.CreateInstance(type);
                            var mappingMethod = type.GetMethod("Mapping");
                            mappingMethod?.Invoke(instance, new object[] { this });
                        }
                        catch (Exception ex)
                        {
                            // Log or handle the error appropriately
                            System.Diagnostics.Debug.WriteLine($"Failed to register mapping for {type.Name}: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
} 