namespace FatCat.Projections.Tests.Objects.SimpleItems;

public class SimpleSource
{
	public double ADouble { get; set; }

	public ushort AShort { get; set; }

	public string? FirstName { get; set; }

	public int Number { get; set; }

	public Guid SimpleId { get; set; }

	public DateTime SomeDate { get; set; }

	public TestingEnum SomeEnum { get; set; }

	public bool SomeSwitch { get; set; }
}