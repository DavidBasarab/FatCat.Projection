using FatCat.Projections.Tests.Objects.OneLevelComplexItems;

namespace FatCat.Projections.Tests.Objects.SimpleItems.ItemsWithLists;

public class DestinationItemWithList
{
	public List<DestinationItemsWithStuff> ItemsWithStuff { get; set; }

	public List<int> Numbers { get; set; }

	public List<SubObject> SubList { get; set; }
}