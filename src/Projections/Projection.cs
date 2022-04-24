using System.Collections;
using FatCat.Projections.Extensions;

namespace FatCat.Projections;

public static class Projection
{
	public static TDestination ProjectTo<TDestination>(object source) where TDestination : class
	{
		var destinationType = typeof(TDestination);

		return (ProjectTo(destinationType, source) as TDestination)!;
	}

	public static object ProjectTo(Type destinationType, object source)
	{
		if (destinationType.IsPrimitive) return source;

		var sourceType = source.GetType();

		if (sourceType.IsList())
		{
			var destinationListType = destinationType.GetGenericArguments()[0];
			
			return ListCopy.Copy(source as IEnumerable, destinationListType);
		}

		var instance = Activator.CreateInstance(destinationType);

		var sourceProperties = sourceType.GetProperties();
		var destinationProperties = destinationType.GetProperties();

		foreach (var sourceProperty in sourceProperties)
		{
			var destinationProperty = destinationProperties.FirstOrDefault(i => i.Name == sourceProperty.Name);

			if (destinationProperty == null || !destinationProperty.CanWrite) continue;

			var typeCode = Type.GetTypeCode(destinationProperty.PropertyType);

			object? propertyValue;
			var sourceValue = sourceProperty.GetValue(source)!;

			if (sourceProperty.IsList())
			{
				var destinationListType = destinationProperty.PropertyType.GetGenericArguments()[0];

				propertyValue = ListCopy.Copy(sourceValue as IEnumerable, destinationListType);
			}
			else propertyValue = ValidSubObject(typeCode, destinationProperty.PropertyType) ? ProjectTo(destinationProperty.PropertyType, sourceValue) : sourceProperty?.GetValue(source);

			destinationProperty.SetValue(instance, propertyValue);
		}

		return instance!;
	}

	private static bool ValidSubObject(TypeCode typeCode, Type type)
	{
		if (type == typeof(TimeSpan)) return false;
		if (type == typeof(DateTime)) return false;

		return type.IsNotAList() && typeCode == TypeCode.Object;
	}
}