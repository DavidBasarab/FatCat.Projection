namespace FatCat.Projections;

public static class Projection
{
	public static TDestination ProjectTo<TDestination>(object source) where TDestination : class
	{
		var customProjection = ProjectionConfiguration.GetCustomProjector<TDestination>();

		if (customProjection != null) return customProjection.ProjectTo(source);

		var destinationType = typeof(TDestination);

		return (ProjectTo(destinationType, source) as TDestination)!;
	}

	public static object? ProjectTo(Type destinationType, object? source)
	{
		if (source == null) return null;

		var customProjection = ProjectionConfiguration.GetCustomProjector(destinationType);

		if (customProjection != null) return customProjection.ProjectTo(source);

		return new ProjectionProcessor(destinationType, source).DoProjection();
	}

	public static void ProjectTo(object destinationObject, object? source) => new ProjectionProcessor(destinationObject.GetType(), source!, destinationObject).DoProjection();
}