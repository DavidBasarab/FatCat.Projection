namespace FatCat.Projections;

public class InvalidProjectionException : Exception
{
    public InvalidProjectionException(Type sourceType, Type destinationType)
        : base($"Invalid projection between <{sourceType.FullName}> to <{destinationType.FullName}>") { }
}
