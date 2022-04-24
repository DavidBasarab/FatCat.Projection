using System.Collections;
using System.Reflection;

namespace FatCat.Projections.Extensions;

internal static class TypeExtensions
{
	public static bool IsBasicType(this Type type)
	{
		var underlyingType = Nullable.GetUnderlyingType(type);

		if (underlyingType != null) return IsBasicType(underlyingType);

		if (type.IsPrimitive) return true;
		if (type == typeof(string)) return true;
		if (type == typeof(TimeSpan)) return true;
		if (type == typeof(DateTime)) return true;

		return false;
	}

	public static bool IsDictionary(this Type type) => type.IsGenericType && type.Implements(typeof(IDictionary<,>));

	public static bool IsList(this Type? type) => type.IsGenericType && type.Implements(typeof(IEnumerable));

	public static bool IsList(this PropertyInfo propertyInfo) => propertyInfo.PropertyType.IsList();

	public static bool IsNotAList(this Type type) => !type.IsList();

	private static bool Implements(this Type? type, Type interfaceType)
	{
		if (type == interfaceType) return false;

		return (interfaceType.IsGenericTypeDefinition && type.GetInterfaces().Where(t => t.IsGenericType).Select(t => t.GetGenericTypeDefinition()).Any(gt => gt == interfaceType)) || interfaceType.IsAssignableFrom(type);
	}

	private static bool ValidSubObject(this Type type, TypeCode typeCode)
	{
		if (type == typeof(TimeSpan)) return false;
		if (type == typeof(DateTime)) return false;

		return type.IsNotAList() && typeCode == TypeCode.Object;
	}
}