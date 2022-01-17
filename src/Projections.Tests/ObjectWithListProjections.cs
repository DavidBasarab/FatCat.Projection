using FatCat.Fakes;
using FatCat.Projections.Tests.Objects.SimpleItems.ItemsWithLists;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests;

public class ObjectWithListProjections
{
	[Fact]
	public void CanProjectWithBasicListItems()
	{
		var source = Faker.Create<SourceItemWithList>();

		var result = Projection.ProjectTo<DestinationItemWithList>(source);

		source.Numbers
			.Should()
			.BeEquivalentTo(result.Numbers);
	}

	[Fact]
	public void CanProjectWithSubObjectList()
	{
		var source = Faker.Create<SourceItemWithList>();

		var result = Projection.ProjectTo<DestinationItemWithList>(source);

		source.SubList
			.Should()
			.BeEquivalentTo(result.SubList);
	}
}