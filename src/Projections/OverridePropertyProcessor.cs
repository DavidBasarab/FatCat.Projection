using System.Reflection;
using Fasterflect;
using FatCat.Projections.Extensions;
using FatCat.Toolkit.Extensions;

namespace FatCat.Projections;

internal class OverridePropertyProcessor
{
    private readonly PropertyInfo[] destinationProperties;
    private readonly object instance;
    private readonly Func<string, object, OverridePropertyValueResult> onPropertySetting;
    private readonly ProjectionSettings projectionSettings;
    private readonly object source;

    public OverridePropertyProcessor(
        object instance,
        object source,
        PropertyInfo[] destinationProperties,
        Func<string, object, OverridePropertyValueResult> getCustomPropertyValue = null,
        ProjectionSettings projectionSettings = ProjectionSettings.None
    )
    {
        this.instance = instance;
        this.source = source;
        this.destinationProperties = destinationProperties;
        this.projectionSettings = projectionSettings;

        if (getCustomPropertyValue != null)
        {
            onPropertySetting = getCustomPropertyValue;
        }
        else
        {
            onPropertySetting = (_, _) => new OverridePropertyValueResult(false);
        }
    }

    public void DoOverrides()
    {
        foreach (var destinationProperty in destinationProperties)
        {
            if (
                destinationProperty.PropertyType.IsNonBasicType()
                && IsTypeSupported(destinationProperty.PropertyType)
            )
            {
                var subObject = destinationProperty.GetValue(instance);

                if (subObject == null)
                {
                    if (!projectionSettings.IsFlagSet(ProjectionSettings.DoNotProjectNull))
                    {
                        subObject = Activator.CreateInstance(destinationProperty.PropertyType);

                        destinationProperty.SetValue(instance, subObject);
                    }
                }

                var overriderMade = false;

                foreach (var subPropertyInfo in destinationProperty.PropertyType.GetProperties())
                {
                    overriderMade |= SetPropertyOnOverride(subPropertyInfo, subObject);
                }

                if (!overriderMade)
                {
                    if (SourceHasProperty(destinationProperty))
                    {
                        var sourceValue = SafeGetPropertyValue(destinationProperty);

                        //If source is null the destination should be null
                        if (
                            sourceValue == null
                            && !projectionSettings.IsFlagSet(ProjectionSettings.DoNotProjectNull)
                        )
                        {
                            destinationProperty.SetValue(instance, null);
                        }
                    }
                }
            }

            SetPropertyOnOverride(destinationProperty, instance);
        }
    }

    private bool IsTypeSupported(Type type)
    {
        return type != typeof(byte[]);
    }

    private object SafeGetPropertyValue(PropertyInfo destinationProperty)
    {
        try
        {
            if (source.GetType().Properties().Any(i => i.Name == destinationProperty.Name))
            {
                return source.GetPropertyValue(destinationProperty.Name);
            }

            return null;
        }
        catch (MissingMemberException)
        {
            return null;
        }
    }

    private bool SetPropertyOnOverride(PropertyInfo propertyInfo, object objectToSet)
    {
        var overrideValue = onPropertySetting(propertyInfo.Name, source);

        if (overrideValue.Found)
        {
            propertyInfo.SetValue(objectToSet, overrideValue.Value);

            return true;
        }

        return false;
    }

    private bool SourceHasProperty(PropertyInfo propertyInfo)
    {
        try
        {
            return source.GetType().Properties().Any(i => i.Name == propertyInfo.Name);
        }
        catch (MissingMemberException)
        {
            return false;
        }
    }
}
