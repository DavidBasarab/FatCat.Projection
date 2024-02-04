namespace FatCat.Projections.Tests.Objects.OneLevelComplexItems;

public class OneLevelDestination
{
	public DifferentSubObject DifferentSubObject { get; set; }

	public SubObject SubObject { get; set; }

	public TimeSpan Time { get; set; }

	public int TimesCreated
	{
		get { return 2; }
	}
}