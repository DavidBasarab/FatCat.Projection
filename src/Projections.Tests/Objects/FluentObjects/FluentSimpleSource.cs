namespace FatCat.Projections.Tests.Objects.FluentObjects;

public class FluentSimpleSource
{
	public string? Name { get; set; }

	public int Number { get; set; }
}

public class FluentSimpleDestination
{
	public int DifferentNumber { get; set; }

	public string? Name { get; set; }
}