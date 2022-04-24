using System.Collections;
using System.Reflection;
using FatCat.Projections.Extensions;

namespace FatCat.Projections;

internal class ProjectionProcessor
{
	private readonly PropertyInfo[] destinationProperties;
	private readonly Type destinationType;
	private readonly object? instance;
	private readonly object source;
	private readonly PropertyInfo[] sourceProperties;
	private readonly Type sourceType;

	internal ProjectionProcessor(Type destinationType, object source)
		: this(destinationType, source, destinationType.IsBasicType() ? null : Activator.CreateInstance(destinationType)) { }

	public ProjectionProcessor(Type destinationType, object source, object? instance)
	{
		this.destinationType = destinationType;
		this.source = source;
		this.instance = instance;

		sourceType = source.GetType();
		sourceProperties = sourceType.GetProperties();
		destinationProperties = destinationType.GetProperties();
	}

	public object DoProjection()
	{
		EnsureProjectionValid();

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
		else propertyValue = ValidSubObject(typeCode, destinationProperty.PropertyType) ? new Projection().ProjectTo(destinationProperty.PropertyType, sourceValue) : sourceProperty?.GetValue(source);

		destinationProperty.SetValue(instance, propertyValue);
	}

	private void EnsureProjectionValid()
	{
		var sourceIsList = sourceType.IsList();
		var destinationIsList = destinationType.IsList();

		if (sourceIsList && !destinationIsList) ThrowInvalidProjection();
		if (!sourceIsList && destinationIsList) ThrowInvalidProjection();

		// if (sourceType.IsBasicType() && !destinationType.IsBasicType()) ThrowInvalidProjection();
		// if (!sourceType.IsBasicType() && destinationType.IsBasicType()) ThrowInvalidProjection();

		var isSourceBasic = sourceType.IsBasicType();
		var isDestinationBasic = destinationType.IsBasicType();

		if (!isSourceBasic && isDestinationBasic) ThrowInvalidProjection();
	}

	private object ProjectList()
	{
		var destinationListType = destinationType.GetGenericArguments()[0];

		return ListCopy.Copy(source as IEnumerable, destinationListType);
	}

	private void ProjectToInstance()
	{
		foreach (var sourceProperty in sourceProperties) AddPropertyValueToInstance(sourceProperty);
	}

	private void ThrowInvalidProjection() { throw new InvalidProjectionException(sourceType, destinationType); }

	private static bool ValidSubObject(TypeCode typeCode, Type type)
	{
		if (type == typeof(TimeSpan)) return false;
		if (type == typeof(DateTime)) return false;

		return type.IsNotAList() && typeCode == TypeCode.Object;
	}
}