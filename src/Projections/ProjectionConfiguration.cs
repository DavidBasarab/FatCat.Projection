using System.Collections.Concurrent;

namespace FatCat.Projections;

public static class ProjectionConfiguration
{
	public static ConcurrentDictionary<Type, Type> CustomProjectors { get; } = new();

	public static void UseCustomProjection<TDestination, TCustomProjector>() where TDestination : class where TCustomProjector : IDoProjection<TDestination> => AddCustomProjector(typeof(TDestination), typeof(TCustomProjector));

	public static void UseCustomProjection<TCustomProjector>(Type destinationType) where TCustomProjector : IDoProjection => AddCustomProjector(destinationType, typeof(TCustomProjector));

	public static IDoProjection GetCustomProjector(Type destinationType)
	{
		if (CustomProjectors.TryGetValue(destinationType, out var projectorType))
		{
			var instance = Activator.CreateInstance(projectorType);
			
			return instance as IDoProjection;
		}

		return null;
	}

	public static IDoProjection<TDestination> GetCustomProjector<TDestination>() where TDestination : class
	{
		if (CustomProjectors.TryGetValue(typeof(TDestination), out var projectorType)) return Activator.CreateInstance(projectorType) as IDoProjection<TDestination>;

		return null;
	}

	private static void AddCustomProjector(Type destinationType, Type customProjector)
	{
		if (CustomProjectors.ContainsKey(destinationType)) CustomProjectors.TryRemove(destinationType, out _);

		CustomProjectors.TryAdd(destinationType, customProjector);
	}
}