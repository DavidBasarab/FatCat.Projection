namespace FatCat.Projections.Extensions;

public class GenericExtensions
{
    public static object CreateListFromType(Type listType)
    {
        var genericListType = typeof(List<>);

        var destinationCombinedType = genericListType.MakeGenericType(listType);

        return Activator.CreateInstance(destinationCombinedType);
    }
}
