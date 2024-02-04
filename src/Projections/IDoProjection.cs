namespace FatCat.Projections;

public interface IDoProjection<TDestination> : IDoProjection
    where TDestination : class
{
    TDestination ProjectTo(object source);
}

public interface IDoProjection
{
    void Project(ref object destinationObject, object source);

    object ProjectToObject(object source);
}
