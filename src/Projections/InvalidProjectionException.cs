namespace FatCat.Projections;

public class InvalidProjectionException(Type sourceType, Type destinationType)
    : Exception($"Invalid projection between <{sourceType.FullName}> to <{destinationType.FullName}>");
