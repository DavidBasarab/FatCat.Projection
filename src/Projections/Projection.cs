namespace FatCat.Projections;

public static class Projection
{
	public static TDestination ProjectTo<TDestination>(object source) where TDestination : class
	{
		var destinationType = typeof(TDestination);

		return (ProjectTo(destinationType, source) as TDestination)!;
	}

	public static object? ProjectTo(Type destinationType, object? source) => source == null ? null : new ProjectionProcessor(destinationType, source).DoProjection();

	public static object? ProjectTo(object destinationObject, object? source) => source == null ? null : new ProjectionProcessor(destinationObject.GetType(), source, destinationObject).DoProjection();
}