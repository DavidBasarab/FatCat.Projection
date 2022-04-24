using FatCat.Fakes;
using FatCat.Projections.Tests.Objects;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests;

public class ProjectToExistingObject
{
	[Fact]
	public void CanGiveAnInstanceAndProjectToIt()
	{
		var sourceItem = Faker.Create<MultiLevelObjectSource>();

		var projectInstance = new MultiLevelObjectSource();

		new Projection().ProjectTo(projectInstance, sourceItem);

		projectInstance
			.Should()
			.BeEquivalentTo(sourceItem);
	}
}