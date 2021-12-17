using FatCat.Projections.Tests.Objects.OneLevelComplexItems;

namespace FatCat.Projections.Tests.Objects;

public class MultiLevelObjectDestination
{
	public DateTime CreatedTime { get; set; }

	public OneLevelDestination Level1 { get; set; }

	public string Name { get; set; }
}