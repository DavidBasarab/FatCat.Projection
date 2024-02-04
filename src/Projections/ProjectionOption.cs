namespace FatCat.Projections;

internal class ProjectionOption<TSource>(
    string destinationMemberName,
    Func<TSource, object> optionValueFunction
)
{
    public string DestinationMemberName { get; } = destinationMemberName;

    public object GetOptionValue(TSource source)
    {
        return optionValueFunction(source);
    }
}
