using System.Reflection;
using FatCat.Projections.Extensions;

namespace FatCat.Projections;

internal class OverridePropertyProcessor
{
	private readonly object instance;
	private readonly object source;
	private readonly PropertyInfo[] destinationProperties;
	private readonly Func<string, object, OverridePropertyValueResult> onPropertySetting;

	public OverridePropertyProcessor(object instance,
									object source,
									PropertyInfo[] destinationProperties,
									Func<string, object, OverridePropertyValueResult>? getCustomPropertyValue = null)
	{
		this.instance = instance;
		this.source = source;
		this.destinationProperties = destinationProperties;

		if (getCustomPropertyValue != null) onPropertySetting = getCustomPropertyValue;
		else onPropertySetting = (_, _) => new OverridePropertyValueResult(false);
	}

	public void DoOverrides()
	{
		foreach (var destinationProperty in destinationProperties)
		{
			if (destinationProperty.PropertyType.IsNonBasicType())
			{
				var subObject = destinationProperty.GetValue(instance);

				if (subObject == null)
				{
					subObject = Activator.CreateInstance(destinationProperty.PropertyType);

					destinationProperty.SetValue(instance, subObject);
				}

				foreach (var subPropertyInfo in destinationProperty.PropertyType.GetProperties()) SetPropertyOnOverride(subPropertyInfo, subObject);
			}

			SetPropertyOnOverride(destinationProperty, instance);
		}
	}

	private void SetPropertyOnOverride(PropertyInfo propertyInfo, object? objectToSet)
	{
		var overrideValue = onPropertySetting(propertyInfo.Name, source);

		// if (overrideValue.Value == null) return;

		if (overrideValue.Found) propertyInfo.SetValue(objectToSet, overrideValue.Value);
	}
}