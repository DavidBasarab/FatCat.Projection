using FatCat.Fakes;
using FatCat.Projections.Tests.Objects;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests;

public class MultiLevelObjectProjection
{
	[Fact]
	public void ProjectToAMultiLevelObject()
	{
		var multiLevelObject = Faker.Create<MultiLevelObject>();

		var result = Projection.ProjectTo<MultiLevelObject>(multiLevelObject);

		result
			.Should()
			.BeEquivalentTo(multiLevelObject);
	}
}