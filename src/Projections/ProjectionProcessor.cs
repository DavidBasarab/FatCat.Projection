using System.Collections;
using System.Reflection;
using FatCat.Projections.Extensions;

namespace FatCat.Projections;

internal class ProjectionProcessor
{
	private readonly PropertyInfo[]? destinationProperties;
	private readonly Type destinationType;
	private readonly object source;
	private readonly PropertyInfo[]? sourceProperties;
	private readonly Type? sourceType;
	private object? instance;

	internal ProjectionProcessor(Type destinationType, object source)
	{
		this.destinationType = destinationType;
		this.source = source;

		sourceType = source.GetType();
		sourceProperties = sourceType.GetProperties();
		destinationProperties = destinationType.GetProperties();
	}

	public object DoProjection()
	{
		if (destinationType.IsPrimitive) return source;

		if (sourceType.IsList()) return ProjectList();

		ProjectToInstance();

		return instance!;
	}

	private void AddPropertyValueToInstance(PropertyInfo sourceProperty)
	{
		var destinationProperty = destinationProperties.FirstOrDefault(i => i.Name == sourceProperty.Name);

		if (destinationProperty == null || !destinationProperty.CanWrite) return;

		var typeCode = Type.GetTypeCode(destinationProperty.PropertyType);

		object? propertyValue;
		var sourceValue = sourceProperty.GetValue(source)!;

		if (sourceProperty.IsList())
		{
			var destinationListType = destinationProperty.PropertyType.GetGenericArguments()[0];

			propertyValue = ListCopy.Copy(sourceValue as IEnumerable, destinationListType);
		}
		else propertyValue = ValidSubObject(typeCode, destinationProperty.PropertyType) ? Projection.ProjectTo(destinationProperty.PropertyType, sourceValue) : sourceProperty?.GetValue(source);

		destinationProperty.SetValue(instance, propertyValue);
	}

	private object ProjectList()
	{
		var destinationListType = destinationType.GetGenericArguments()[0];

		return ListCopy.Copy(source as IEnumerable, destinationListType);
	}

	private void ProjectToInstance()
	{
		instance = Activator.CreateInstance(destinationType);

		foreach (var sourceProperty in sourceProperties) AddPropertyValueToInstance(sourceProperty);
	}

	private static bool ValidSubObject(TypeCode typeCode, Type type)
	{
		if (type == typeof(TimeSpan)) return false;
		if (type == typeof(DateTime)) return false;

		return type.IsNotAList() && typeCode == TypeCode.Object;
	}
}