namespace FatCat.Projections;

internal class OverridePropertyValueResult
{
	public bool Found { get; set; }

	public object Value { get; set; }

	public OverridePropertyValueResult(bool found, object value = null)
	{
		Found = found;
		Value = value;
	}
}