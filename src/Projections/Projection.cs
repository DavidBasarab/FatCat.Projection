using FatCat.Toolkit.Extensions;

namespace FatCat.Projections;

public static class Projection
{
    public static TDestination ProjectTo<TDestination>(
        object source,
        ProjectionSettings settings = ProjectionSettings.None
    )
        where TDestination : class
    {
        var customProjection = ProjectionConfiguration.GetCustomProjector<TDestination>();

        if (DoNotSkipCustom(settings) && customProjection is not null)
        {
            return customProjection.ProjectTo(source);
        }

        var destinationType = typeof(TDestination);

        return (ProjectTo(destinationType, source, settings) as TDestination)!;
    }

    public static object ProjectTo(
        Type destinationType,
        object source,
        ProjectionSettings settings = ProjectionSettings.None
    )
    {
        if (source == null)
        {
            return null;
        }

        var customProjection = ProjectionConfiguration.GetCustomProjector(destinationType);

        if (DoNotSkipCustom(settings) && customProjection != null)
        {
            return customProjection.ProjectToObject(source);
        }

        return new ProjectionProcessor(destinationType, source).DoProjection();
    }

    public static void ProjectTo(
        ref object destinationObject,
        object source,
        ProjectionSettings settings = ProjectionSettings.None
    )
    {
        var customProjection = ProjectionConfiguration.GetCustomProjector(destinationObject.GetType());

        if (DoNotSkipCustom(settings) && customProjection != null)
        {
            customProjection.Project(ref destinationObject, source);

            return;
        }

        new ProjectionProcessor(
            destinationObject.GetType(),
            source!,
            destinationObject,
            settings
        ).DoProjection();
    }

    private static bool DoNotSkipCustom(ProjectionSettings settings)
    {
        return !settings.IsFlagSet(ProjectionSettings.SkipCustomProjector);
    }
}
