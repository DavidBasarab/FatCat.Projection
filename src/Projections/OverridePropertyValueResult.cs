namespace FatCat.Projections;

internal class OverridePropertyValueResult(bool found, object value = null)
{
    public bool Found { get; set; } = found;

    public object Value { get; set; } = value;
}
