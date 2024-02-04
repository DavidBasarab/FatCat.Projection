namespace FatCat.Projections;

public interface IProjector
{
    TDestination ProjectTo<TDestination>(object source)
        where TDestination : class
    {
        return ProjectTo<TDestination>(source, ProjectionSettings.None);
    }

    object ProjectTo(Type destinationType, object source)
    {
        return ProjectTo(destinationType, source, ProjectionSettings.None);
    }

    void ProjectTo(ref object destinationObject, object source)
    {
        ProjectTo(ref destinationObject, source, ProjectionSettings.None);
    }

    TDestination ProjectTo<TDestination>(object source, ProjectionSettings settings)
        where TDestination : class;

    object ProjectTo(Type destinationType, object source, ProjectionSettings settings);

    void ProjectTo(ref object destinationObject, object source, ProjectionSettings settings);
}

public class Projector : IProjector
{
    public TDestination ProjectTo<TDestination>(
        object source,
        ProjectionSettings settings = ProjectionSettings.None
    )
        where TDestination : class
    {
        return Projection.ProjectTo<TDestination>(source, settings);
    }

    public object ProjectTo(
        Type destinationType,
        object source,
        ProjectionSettings settings = ProjectionSettings.None
    )
    {
        return Projection.ProjectTo(destinationType, source, settings);
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
