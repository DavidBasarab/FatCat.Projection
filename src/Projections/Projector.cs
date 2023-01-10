namespace FatCat.Projections;

public interface IProjector
{
	TDestination ProjectTo<TDestination>(object source) where TDestination : class;

	object? ProjectTo(Type destinationType, object source);

	void ProjectTo(object destinationObject, object source);
}

public class Projector : IProjector
{
	public TDestination ProjectTo<TDestination>(object source) where TDestination : class => Projection.ProjectTo<TDestination>(source);

	public object? ProjectTo(Type destinationType, object source) => Projection.ProjectTo(destinationType, source);

	public void ProjectTo(object destinationObject, object source) => Projection.ProjectTo(destinationObject, source);
}