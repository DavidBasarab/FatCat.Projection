namespace FatCat.Projections;

public class Projection : IProjection
{
	public TDestination ProjectTo<TDestination>(object source) where TDestination : class
	{
		var destinationType = typeof(TDestination);

		return (ProjectTo(destinationType, source) as TDestination)!;
	}

	public object ProjectTo(Type destinationType, object source) => new ProjectionProcessor(destinationType, source).DoProjection();

	public object ProjectTo(object destinationObject, object source) => new ProjectionProcessor(destinationObject.GetType(), source, destinationObject).DoProjection();
}