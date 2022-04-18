using FatCat.Projections.Tests.Objects.OneLevelComplexItems;

namespace FatCat.Projections.Tests.Objects;

public class MultiLevelObjectSource
{
	public DateTime CreatedTime { get; set; }

	public OneLevelSource? Level1 { get; set; }

	public string? Name { get; set; }
}