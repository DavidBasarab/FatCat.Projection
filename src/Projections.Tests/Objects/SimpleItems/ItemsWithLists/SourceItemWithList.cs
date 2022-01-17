using FatCat.Projections.Tests.Objects.OneLevelComplexItems;

namespace FatCat.Projections.Tests.Objects.SimpleItems.ItemsWithLists;

public class SourceItemWithList
{
	public List<int> Numbers { get; set; }

	public List<SubObject> SubList { get; set; }
}