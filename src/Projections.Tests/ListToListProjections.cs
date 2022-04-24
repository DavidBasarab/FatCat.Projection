using FatCat.Fakes;
using FatCat.Projections.Tests.Objects.SimpleItems.ItemsWithLists;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests;

public class ListToListProjections
{
	[Fact]
	public void CanProjectFromListOfSameType()
	{
		var sourceList = Faker.Create<List<SourceItemsWithStuff>>();

		var resultList = Projection.ProjectTo<List<SourceItemsWithStuff>>(sourceList);

		resultList
			.Should()
			.BeEquivalentTo(sourceList);
	}
}