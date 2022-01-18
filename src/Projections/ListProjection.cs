namespace FatCat.Projections;

internal class ListProjection
{
	private readonly Type sourceType;
	private readonly Type destinationType;
	private readonly object sourceValue;

	public static object ProjectTo(Type sourceType, Type destinationType, object sourceValue)
	{
		return new ListProjection(sourceType, destinationType, sourceValue);
	}

	public ListProjection(Type sourceType, Type destinationType, object sourceValue)
	{
		this.sourceType = sourceType;
		this.destinationType = destinationType;
		this.sourceValue = sourceValue;
	}

	private object DoProjection()
	{
		return null;
	}
}