using System.Reflection;
using EcomAPI.Common.ServiceInstallers.Attributes.Base;

namespace EcomAPI.Common.ServiceInstallers
{
    public static class MappedTypesFactory
    {
        public static IEnumerable<KeyValuePair<Type, Type>> GetMappedTypesFromAssemblies<TType>(params Assembly[] assemblies)
                 where TType : Attribute, IServiceAttributeProperties
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                    .Where(x => x.GetCustomAttribute(typeof(TType)) != null)
                    .OrderByDescending(x => ((IServiceAttributeProperties)x.GetCustomAttribute(typeof(TType))!).Ordinance);

                foreach (var type in types)
                    yield return new KeyValuePair<Type, Type>(GetBaseType(type), type);
            }
        }

        private static Type GetBaseType(Type type)
        {
            var interfaces = type.GetTypeInfo().GetInterfaces();

            if (interfaces.Length == 0 || interfaces.Length > 1)
                return type;

            return interfaces[0];
        }
    }
}