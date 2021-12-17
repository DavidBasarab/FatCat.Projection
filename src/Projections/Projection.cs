namespace FatCat.Projections;

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

			if (destinationProperty == null || !destinationProperty.CanWrite)
			{
				continue;
			}

			var typeCode = Type.GetTypeCode(destinationProperty.PropertyType);

			var propertyValue = ValidSubObject(typeCode, destinationProperty.PropertyType) ? ProjectTo(destinationProperty.PropertyType, sourceProperty.GetValue(source)!) : sourceProperty?.GetValue(source);

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