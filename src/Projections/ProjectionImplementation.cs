namespace FatCat.Projections;

public class ProjectionImplementation : IProjection
{
	public TDestination ProjectTo<TDestination>(object source) where TDestination : class => Projection.ProjectTo<TDestination>(source);

	public object ProjectTo(Type destinationType, object source) => Projection.ProjectTo(destinationType, source);
}