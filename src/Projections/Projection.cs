using FatCat.Toolkit.Console;

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

	public static object ProjectTo(Type destinationType, object source)
	{
		if (source == null) return null;

		var customProjection = ProjectionConfiguration.GetCustomProjector(destinationType);

		if (customProjection != null)
		{
			ConsoleLog.WriteCyan($"Using Custom Projection for <{source.GetType().FullName}> | Projector <{customProjection.GetType().FullName}>");

			return customProjection.ProjectToObject(source);
		}

		return new ProjectionProcessor(destinationType, source).DoProjection();
	}

	public static void ProjectTo(ref object destinationObject, object source, ProjectionSettings settings = ProjectionSettings.None)
	{
		var customProjection = ProjectionConfiguration.GetCustomProjector(destinationObject.GetType());

		if (customProjection != null)
		{
			ConsoleLog.WriteCyan($"Using Custom Projection for <{destinationObject.GetType().FullName}> | Projector <{customProjection.GetType().FullName}>");

			customProjection.Project(ref destinationObject, source);

			return;
		}

		new ProjectionProcessor(destinationObject.GetType(), source!, destinationObject, settings).DoProjection();
	}
}