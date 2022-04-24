namespace FatCat.Projections;

public class Projection
{
	public static TDestination ProjectTo<TDestination>(object source) where TDestination : class
	{
		var destinationType = typeof(TDestination);

		return (ProjectTo(destinationType, source) as TDestination)!;
	}

	public static object ProjectTo(Type destinationType, object source) => new ProjectionProcessor(destinationType, source).DoProjection();
}