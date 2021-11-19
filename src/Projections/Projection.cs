using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FatCat.Projections
{
	internal static class TypeExtensions
	{
		private static bool Implements(this Type type, Type interfaceType)
		{
			if (type == interfaceType) return false;

			return interfaceType.IsGenericTypeDefinition && type.GetInterfaces().Where(t => t.IsGenericType).Select(t => t.GetGenericTypeDefinition()).Any(gt => gt == interfaceType) || interfaceType.IsAssignableFrom(type);
		}

		public static bool IsDictionary(this Type type) => type.IsGenericType && type.Implements(typeof(IDictionary<,>));

		public static bool IsList(this Type type) => type.IsGenericType && type.Implements(typeof(IEnumerable));

		public static bool IsNotAList(this Type type) => !type.IsList();
	}

	public static class Projection
	{
		public static TDestination ProjectTo<TDestination>(object source) where TDestination : class
		{
			var destinationType = typeof(TDestination);

			return (ProjectTo(destinationType, source) as TDestination)!;
		}

		private static object ProjectTo(Type destinationType, object source)
		{
			var instance = Activator.CreateInstance(destinationType);

			var sourceType = source.GetType();

			var sourceProperties = sourceType.GetProperties();
			var destinationProperties = destinationType.GetProperties();

			foreach (var sourceProperty in sourceProperties)
			{
				var destinationProperty = destinationProperties.FirstOrDefault(i => i.Name == sourceProperty.Name);

				if (destinationProperty == null) continue;

				var typeCode = Type.GetTypeCode(destinationProperty.PropertyType);

				if (destinationProperty.PropertyType.IsNotAList() && typeCode == TypeCode.Object)
				{
					var propertyValue = ProjectTo(destinationProperty.PropertyType, sourceProperty.GetValue(source)!);

					destinationProperty.SetValue(instance, propertyValue);
				}
				else destinationProperty?.SetValue(instance, sourceProperty.GetValue(source));
			}

			return instance!;
		}
	}
}