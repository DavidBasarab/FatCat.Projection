namespace FatCat.Projections.Tests.Objects
{
	public class SimpleDestination
	{
		public string? FirstName { get; set; }

		public int Number { get; set; }
	}

	public class SimpleDestinationMissingProperty
	{
		public int Number { get; set; }
	}
}