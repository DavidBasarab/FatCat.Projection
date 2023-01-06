using System.Reflection;
using Fasterflect;
using FatCat.Projections.Extensions;

namespace FatCat.Projections;

internal class OverridePropertyProcessor
{
	private readonly PropertyInfo[] destinationProperties;
	private readonly object instance;
	private readonly Func<string, object, OverridePropertyValueResult> onPropertySetting;
	private readonly object source;

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
			if (destinationProperty.PropertyType.IsNonBasicType() && IsTypeSupported(destinationProperty.PropertyType))
			{
				var subObject = destinationProperty.GetValue(instance);

				if (subObject == null)
				{
					subObject = Activator.CreateInstance(destinationProperty.PropertyType);

					destinationProperty.SetValue(instance, subObject);
				}

				var overriderMade = false;

				foreach (var subPropertyInfo in destinationProperty.PropertyType.GetProperties()) overriderMade |= SetPropertyOnOverride(subPropertyInfo, subObject);

				if (!overriderMade)
				{
					var sourceValue = source.GetPropertyValue(destinationProperty.Name);

					// If source is null the destination should be null
					if (sourceValue == null) destinationProperty.SetValue(instance, null);
				}
			}

			SetPropertyOnOverride(destinationProperty, instance);
		}
	}

	private bool IsTypeSupported(Type type) => type != typeof(byte[]);

	private bool SetPropertyOnOverride(PropertyInfo propertyInfo, object? objectToSet)
	{
		var overrideValue = onPropertySetting(propertyInfo.Name, source);

		if (overrideValue.Found)
		{
			propertyInfo.SetValue(objectToSet, overrideValue.Value);

			return true;
		}

		return false;
	}
}