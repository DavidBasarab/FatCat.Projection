using FatCat.Projections.Tests.Objects.OneLevelComplexItems;

namespace FatCat.Projections.Tests.Objects;

public class MultiLevelObject
{
	public DateTime CreatedTime { get; set; }

	public OneLevelSource LevelSource1 { get; set; }

	public string Name { get; set; }
}