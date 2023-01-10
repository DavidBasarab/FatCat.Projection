namespace FatCat.Projections;

public interface IDoProjection<TDestination> where TDestination : class
{
	void Project(TDestination destinationObject, object source);

	TDestination ProjectTo(object source);
}

public interface IDoProjection
{
	void Project(object destinationObject, object source);

	object ProjectTo(object source);
}