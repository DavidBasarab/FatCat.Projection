namespace FatCat.Projections;

public interface IProjection
{
	public TDestination ProjectTo<TDestination>(object source) where TDestination : class;

	public object ProjectTo(Type destinationType, object source);
}