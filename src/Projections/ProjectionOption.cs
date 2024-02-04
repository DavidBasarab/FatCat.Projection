namespace FatCat.Projections;

internal class ProjectionOption<TSource>
{
    private readonly Func<TSource, object> optionValueFunction;

    public string DestinationMemberName { get; }

    public ProjectionOption(string destinationMemberName, Func<TSource, object> optionValueFunction)
    {
        DestinationMemberName = destinationMemberName;
        this.optionValueFunction = optionValueFunction;
    }

    public object GetOptionValue(TSource source)
    {
        return optionValueFunction(source);
    }
}
