namespace FatCat.Projections;

public interface IDoProjection<TDestination> where TDestination : class
{
	void Project(TDestination destinationObject, object source);

	TDestination ProjectTo(object source);
}