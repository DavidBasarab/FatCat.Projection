using System.Collections.Concurrent;

namespace FatCat.Projections;

public static class ProjectionConfiguration
{
	private static ConcurrentDictionary<Type, Type> CustomProjectors { get; } = new();

	public static void UseCustomProjection<TDestination, TCustomProjector>() where TDestination : class where TCustomProjector : IDoProjection<TDestination>
	{
		if (CustomProjectors.ContainsKey(typeof(TDestination))) CustomProjectors.TryRemove(typeof(TDestination), out _);

		CustomProjectors.TryAdd(typeof(TDestination), typeof(TCustomProjector));
	}

	internal static IDoProjection<TDestination>? GetCustomProjector<TDestination>() where TDestination : class
	{
		if (CustomProjectors.TryGetValue(typeof(TDestination), out var projectorType)) return Activator.CreateInstance(projectorType) as IDoProjection<TDestination>;

		return null;
	}
}