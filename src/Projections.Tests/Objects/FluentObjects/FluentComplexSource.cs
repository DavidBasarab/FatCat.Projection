namespace FatCat.Projections.Tests.Objects.FluentObjects;

public class FluentComplexSource
{
	public FluentSimpleSource? Simple { get; set; }

	public string? Title { get; set; }
}

public class FluentComplexDestination
{
	public string? FullTitle { get; set; }

	public int TheNumber { get; set; }
}