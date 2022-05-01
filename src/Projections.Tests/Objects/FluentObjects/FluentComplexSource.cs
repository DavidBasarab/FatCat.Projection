namespace FatCat.Projections.Tests.Objects.FluentObjects;

public class FluentComplexSource
{
	public FluentSimpleSource? Simple { get; set; }

	public string? Title { get; set; }
}

public class FluentKindOfSimpleDestination
{
	public string? FullTitle { get; set; }

	public int TheNumber { get; set; }
}

public class FluentComplexDestination
{
	public FluentKindOfSimpleDestination SubObject { get; set; }

	public string Name { get; set; }
}