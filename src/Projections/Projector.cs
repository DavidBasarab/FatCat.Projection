namespace FatCat.Projections;

public interface IProjector
{
    TDestination ProjectTo<TDestination>(object source)
        where TDestination : class;

    object ProjectTo(Type destinationType, object source);

    void ProjectTo(
        ref object destinationObject,
        object source,
        ProjectionSettings settings = ProjectionSettings.None
    );
}

public class Projector : IProjector
{
    public TDestination ProjectTo<TDestination>(object source)
        where TDestination : class
    {
        return Projection.ProjectTo<TDestination>(source);
    }

    public object ProjectTo(Type destinationType, object source)
    {
        return Projection.ProjectTo(destinationType, source);
    }

    public void ProjectTo(
        ref object destinationObject,
        object source,
        ProjectionSettings settings = ProjectionSettings.None
    )
    {
        Projection.ProjectTo(ref destinationObject, source, settings);
    }
}
